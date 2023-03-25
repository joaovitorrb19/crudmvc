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
            var auxPrimeiro = await context.Clientes.FindAsync(id);
            if(auxPrimeiro == null){
                return RedirectToAction("Index","Cliente");
            } else {
            var aux = await context.Enderecos.Include(x => x.cliente).Where(x => x.ClienteId == id).AsNoTracking().ToListAsync();
            TempData["ClienteIdSelecionado"] = id ;
            return View(aux);
            }
            
        }


        [HttpGet]
        public async Task<ActionResult> Cadastrar(int? id){ ;
            var aux = await _repository.BuscarPorId(id);
            if(aux != null){
                return View(aux);
            } else {
                return View();
            }  
        }
        [HttpPost]
        public async Task<ActionResult> Cadastrar([FromForm]EnderecoModel endereco){

            var aux = await RetornarEnderecoModelService.GerarEnderecoModel(endereco.cep,endereco.complemento);

            EnderecoModel end = new EnderecoModel{
                 cep = aux.cep,
                 logradouro = aux.logradouro,
                complemento = endereco.complemento,
                localidade = aux.localidade,
                uf = aux.uf,
                ddd = aux.ddd,
                bairro = aux.bairro,
                ClienteId = (int)TempData["ClienteIdSelecionado"]
                
            };

                var auxEndereco = await _repository.BuscarPorId(endereco.EnderecoId);
                
                if(auxEndereco != null && aux != null){
                    end.EnderecoId = endereco.EnderecoId;
                    end.ClienteId = (int)TempData["ClienteIdSelecionado"];
                    end.complemento = endereco.complemento;
                    _repository.Atualizar(end);
                    await _unit.Salvar();

                } else if (_repository.VerificarExistenciaPorCep(endereco.cep,(int)TempData["ClienteIdSelecionado"]).Result == false && aux != null) {
                    aux.ClienteId = (int)TempData["ClienteIdSelecionado"];
                    _repository.Adicionar(end);
                    await _unit.Salvar();
                     
                }
                     return RedirectToAction("Index", new { id = end.ClienteId});
        }

        public async Task<ActionResult> Excluir(int id,[FromServices]DataContext context){
            var aux = await _repository.BuscarPorId(id);
            await _repository.Excluir(id);
            await _unit.Salvar();
           
            return RedirectToAction("Index",new {id = aux.ClienteId});
        }

    }
}