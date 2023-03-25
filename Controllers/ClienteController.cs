using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;
using ProjetoCurso.Services;

namespace ProjetoCurso.Controllers {
    public class ClienteController : Controller {

        private readonly IClienteRepository _repository;
        private readonly UnitOfWork _unit;

        public ClienteController(IClienteRepository repository,UnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<ActionResult> Index(){
            var auxClientes = await _repository.BuscarTodos();
            if(TempData["Mensagem"] != null){
                var teste2 = JsonSerializer.Deserialize<MensagemPartialModel>(TempData["Mensagem"].ToString());
                ViewData["Mensagem"] = teste2;
            }
    

            return View(auxClientes);
        }

        [HttpGet]
        public async Task<ActionResult> Cadastrar(int? id){
            var auxClienteExiste = await _repository.BuscarPorId(id);
            if(auxClienteExiste != null){
                return View(auxClienteExiste);
            } else {
                return View();
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar([FromForm]ClienteModel cliente){

            if(ModelState.IsValid){
               
                var auxClienteExiste = await _repository.BuscarPorId(cliente.ClienteId);
                if (auxClienteExiste != null){
                    _repository.Atualizar(cliente);
                   await _unit.Salvar();
                   TempData["Mensagem"] = PopularPartialMensagemService.Serializar($"Cliente {cliente.NomeCliente} Atualizado com Sucesso!");
                } else if(_repository.VerificarExistenciaPorCpf(cliente.CPFCliente).Result == false){
                    _repository.Adicionar(cliente);
                    await _unit.Salvar();
                     TempData["Mensagem"] = PopularPartialMensagemService.Serializar($"Cliente {cliente.NomeCliente} Criado com Sucesso!");
                } else {
                    ViewData["Mensagem"] = PopularPartialMensagemService.RetornarPartial($"O CPF {cliente.CPFCliente} já existe... Por favor Digite Outro","Erro");
                    return View();
                }
            }
            
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Excluir(int id){
            var teste = await _repository.BuscarPorId(id);
            _repository.Excluir(id);
            await _unit.Salvar();
            TempData["Mensagem"] = PopularPartialMensagemService.Serializar($"Cliente {teste.NomeCliente} excluido com sucesso!");
            return RedirectToAction("Index");
        }
    }
}