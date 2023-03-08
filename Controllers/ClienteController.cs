using Microsoft.AspNetCore.Mvc;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;

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
                } else {
                    _repository.Adicionar(cliente);
                    await _unit.Salvar();
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Excluir(int id){
            _repository.Excluir(id);
            await _unit.Salvar();
            return RedirectToAction("Index");
        }
    }
}