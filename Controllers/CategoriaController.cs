using Microsoft.AspNetCore.Mvc;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;

namespace ProjetoCurso.Controllers {
    public class CategoriaController : Controller {

        private readonly ICategoriaRepository _repository;
        private readonly UnitOfWork _unit;

        public CategoriaController(ICategoriaRepository repository,UnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<ActionResult> Index(){
            return View(await _repository.BuscarTodos());
        }

        [HttpGet]
        public async Task<ActionResult> Cadastrar(int? id){
             var auxCategoria = await _repository.BuscarPorId(id);
             if(auxCategoria != null){
                return View(auxCategoria);
             }  else {
                return View();
             }
            
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(CategoriaModel categoria){

                if(ModelState.IsValid){
                    var auxCategoria = await _repository.BuscarPorId(categoria.CategoriaId); 
                    if(auxCategoria != null){
                        _repository.Atualizar(categoria);
                        await _unit.Salvar();
                    } else {
                        await _repository.Adicionar(categoria);
                         await _unit.Salvar();
                    }
           }
           
           return RedirectToAction("Index");
        }

        public async Task<ActionResult> Excluir(int id){
            try{
                _repository.Excluir(id);
                await _unit.Salvar();
            } catch (Exception e){
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Index");
        }
        
    }
}