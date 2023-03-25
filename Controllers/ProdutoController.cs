using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;
using ProjetoCurso.Services;

namespace ProjetoCurso.Controllers {
    public class ProdutoController : Controller {

        private readonly IProdutoRepository _repository;

        private readonly UnitOfWork _unit;

        public ProdutoController(IProdutoRepository repository,UnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }


        public async Task<ActionResult> Index([FromServices]DataContext context){
            if(TempData["Mensagem"] == null){

            } else {
                ViewData["Mensagem"] = PopularPartialMensagemService.Desserializar(TempData["Mensagem"].ToString());
            }
            var auxProdutos = await context.Produtos.Include(x => x.Categoria).ToListAsync();
            return View(auxProdutos);
        }

        [HttpGet]
        public async Task<ActionResult> Cadastrar(int? id,[FromServices]DataContext context){
            var auxProduto = await _repository.BuscarPorId(id);
             SelectList listaCategorias = new SelectList(context.Categorias.ToList(),nameof(Model.CategoriaModel.CategoriaId),nameof(Model.CategoriaModel.NomeCategoria));
             ViewBag.Categorias = listaCategorias;
                if(auxProduto != null){
                    return View(auxProduto);
                } else {
                    return View();
                }   
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar([FromForm]ProdutoModel produto){
        
           if(ModelState.IsValid){
              if(_repository.VerificarExistenciaPorNome(produto.NomeProduto).Result == false) {
                    var auxProduto = await _repository.BuscarPorId(produto.ProdutoId); 
                    if(auxProduto != null){
                        _repository.Atualizar(produto);
                        await _unit.Salvar();
                        TempData["Mensagem"] = PopularPartialMensagemService.Serializar($"O produto {produto.NomeProduto} foi atualizado com sucesso!");
                    } else {
                         _repository.Adicionar(produto);
                         await _unit.Salvar();
                         TempData["Mensagem"] = PopularPartialMensagemService.Serializar($"O produto {produto.NomeProduto} foi cadastrado com sucesso!");
                    }
           } else {
            ViewData["Mensagem"] = PopularPartialMensagemService.RetornarPartial($"O produto {produto.NomeProduto} ja existe ...","Erro");
            return View();
           }
        }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Excluir(int id){
            var teste = await _repository.BuscarPorId(id);
            _repository.Excluir(id);
            await _unit.Salvar();
            TempData["Mensagem"] = PopularPartialMensagemService.Serializar($"O Produto {teste.NomeProduto} foi excluido com sucesso!");
            return RedirectToAction("Index");
        }
    }
}