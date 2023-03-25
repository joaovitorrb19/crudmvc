using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;

namespace ProjetoCurso.Controllers {
    public class ItemPedidoController : Controller {

            private readonly IItemPedidoRepository _repository;

            private readonly DataContext _context;
            private readonly UnitOfWork _unit;

            public ItemPedidoController(IItemPedidoRepository repository,UnitOfWork unit,DataContext context)
            {
                _repository = repository;
                _unit = unit;
                _context = context;
            }


            public async Task<ActionResult> AdicionarItemNaLista(ItemPedidoModel item){
                var auxProduto = await _context.Produtos.FindAsync(item.ProdutoId);
                var auxPedido = await _context.Pedidos.FindAsync(item.PedidoId);

                if(item.ItemPedidoId > 0){
                    var auxItemPedido = await _repository.BuscarPorId(item.ItemPedidoId);
                    auxPedido.ValorTotal -= (auxProduto.PrecoProduto * auxItemPedido.Quantidade);
                    _repository.Atualizar(item);

                    auxPedido.ValorTotal += (auxProduto.PrecoProduto * item.Quantidade);
                } else {
                    _repository.Adicionar(item);
                    auxPedido.ValorTotal += (auxProduto.PrecoProduto * item.Quantidade);
                }
                    
                    await _unit.Salvar();
                    return RedirectToAction("EditarPedido","Pedido",new {idPedido = item.PedidoId});
            }

            public async Task<ActionResult> ExcluirItemNaLista(int iditempedido,int idpedido){
            var auxItemPedido = await _context.ItensPedidos.FindAsync(iditempedido);
            var auxProduto = await _context.Produtos.FindAsync(auxItemPedido.ProdutoId);
            var auxPedido = await _context.Pedidos.FindAsync(idpedido);
            auxPedido.ValorTotal -= (auxProduto.PrecoProduto * auxItemPedido.Quantidade);
            _context.ItensPedidos.Remove(auxItemPedido);
            await _unit.Salvar();
            return RedirectToAction("EditarPedido","Pedido",new {idPedido = idpedido});
            }


    }
}