using Microsoft.AspNetCore.Mvc;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;
using ProjetoCurso.Services;

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
            if(TempData["Mensagem"] == null){

            } else {
                ViewData["Mensagem"] = PopularPartialMensagemService.Desserializar(TempData["Mensagem"].ToString());
            }
            
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
                    if(_repository.VerificarExistenciaPorNome(categoria.NomeCategoria).Result == false){
                    var auxCategoria = await _repository.BuscarPorId(categoria.CategoriaId);
                    if(auxCategoria != null){
                        _repository.Atualizar(categoria);
                        await _unit.Salvar();
                        TempData["Mensagem"] = PopularPartialMensagemService.Serializar($"A categoria {categoria.NomeCategoria} foi atualizada com sucesso!");
                    } else {
                         await _repository.Adicionar(categoria);
                         TempData["Mensagem"] = PopularPartialMensagemService.Serializar($"A categoria {categoria.NomeCategoria} foi criada com sucesso!");
                         await _unit.Salvar();
                    }
                 } else{
                    ViewData["Mensagem"] = PopularPartialMensagemService.RetornarPartial($"A categoria {categoria.NomeCategoria} já existe...","Erro");
                    return View();
                }
                }
           
           return RedirectToAction("Index");
        }

        public async Task<ActionResult> Excluir(int id){
            try{
                var teste = await _repository.BuscarPorId(id);
                _repository.Excluir(id);
                await _unit.Salvar();
                TempData["Mensagem"] = PopularPartialMensagemService.Serializar($"A categoria {teste.NomeCategoria} foi excluida com sucesso!");
            } catch (Exception e){
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Index");
        }
        
    }
}