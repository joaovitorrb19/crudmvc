using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;

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
                    var auxProduto = await _repository.BuscarPorId(produto.ProdutoId); 
                    if(auxProduto != null){
                        _repository.Atualizar(produto);
                        await _unit.Salvar();
                    } else {
                         _repository.Adicionar(produto);
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