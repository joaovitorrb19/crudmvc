using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;
using ProjetoCurso.Services;

namespace ProjetoCurso.Controllers {
    public class EnderecoController : Controller {

        private readonly IEnderecoRepository _repository;
        private readonly UnitOfWork _unit;

        public EnderecoController(IEnderecoRepository repository,UnitOfWork unit)
        {   
            _repository = repository;
            _unit = unit;
        }


        public async Task<ActionResult> Index(int id,[FromServices]DataContext context){
            var aux = await context.Enderecos.Include(x => x.cliente).AsNoTracking().ToListAsync();
            return View(aux);
        }


        [HttpGet]
        public async Task<ActionResult> Cadastrar(int? id){
            var aux = await _repository.BuscarPorId(id);
            if(aux != null){
                return View(aux);
            } else {
                return View();
            }  
        }
        [HttpPost]
        public async Task<ActionResult> Cadastrar([FromForm]EnderecoModel endereco){
            HttpClient httpTeste = new HttpClient();
            var aux = await httpTeste.GetAsync("https://viacep.com.br/ws/74565150/json/");
            var auxResp = await aux.Content

            if(ModelState.IsValid){
                var auxEndereco = await _repository.BuscarPorId(endereco.EnderecoId);
                
                if(auxEndereco != null){
                    _repository.Atualizar(endereco);
                    await _unit.Salvar();
                } else {
                    _repository.Adicionar(endereco);
                    await _unit.Salvar();
                }
            }

            return RedirectToAction("Index");
        }
    }
}