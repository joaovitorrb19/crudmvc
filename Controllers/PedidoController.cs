using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;

namespace ProjetoCurso.Controllers {
    public class PedidoController : Controller {
        
        private readonly IPedidoRepository _repository;
        private DataContext _context;
        private readonly UnitOfWork _unit; 
        public PedidoController(IPedidoRepository repository,UnitOfWork unit,DataContext context)
        {
            _repository = repository;
            _unit = unit;
            _context = context;
        }

        public async Task<ActionResult> Index(int idCliente){
            var auxListaPedidos = await _repository.BuscarTodos(idCliente);
            var auxCliente = await _context.Clientes.FindAsync(idCliente);
            ViewBag.NomeClienteIndex =  auxCliente.NomeCliente;
            ViewBag.IdClienteIndex = idCliente;
            return View(auxListaPedidos);
        }

        public async Task<ActionResult> Cadastrar(int idCliente){

            if(ModelState.IsValid){
                    PedidoModel pedido = new PedidoModel{ClienteId = idCliente};
                    _repository.Adicionar(pedido);
                    await _unit.Salvar();
            }

            return RedirectToAction("Index",new { idCliente = idCliente });
        }

        public async Task<ActionResult> EditarPedido(int idPedido){
            var auxPedido = await _context.Pedidos.FindAsync(idPedido);
            var auxCliente = await _context.Clientes.FirstOrDefaultAsync(x => x.ClienteId == auxPedido.ClienteId);
            var aux = await _context.ItensPedidos.AsNoTracking().Where(x => x.PedidoId == idPedido).Include(x => x.Produto).ToListAsync();
            ViewBag.IdPedidoEditar = idPedido;
            ViewBag.IdClienteEditarPedido = auxCliente.ClienteId;
            return View(aux);
        }

        public async Task<ActionResult> AdicionarItemPedido(int idPedido,int? idItemPedido){
            List<ProdutoModel> listaProdutos = await _context.Produtos.ToListAsync();
            ViewBag.ListaProdutos = listaProdutos.Select(p => new SelectListItem{
                Value = p.ProdutoId.ToString(),
                Text = $"{p.NomeProduto}({p.PrecoProduto.ToString("C2")} Unid.)"
            }).ToList();
            ViewBag.IdPedidoAdicionar = idPedido;
            if(idItemPedido != null){
                var aux = await _context.ItensPedidos.FindAsync(idItemPedido);
                return View(aux);
            } else {
                return View();
            }
        }
        public async Task<ActionResult> Fechar(int idCliente,int idPedido){
            _repository.FecharPedido(idPedido);
            await _unit.Salvar();
            return RedirectToAction("Index",new {idCliente = idCliente});

        }

        public async Task<ActionResult> EntregarListaEndereco(int idCliente, int idPedido){
            var auxEnderecos =  await _context.Enderecos.Where(x => x.ClienteId == idCliente).ToListAsync();
            var auxPedido = await _context.Pedidos.Include(x => x.Cliente).FirstOrDefaultAsync(x => x.PedidoId == idPedido);
            ViewBag.DadosPedido = auxPedido;
            return View(auxEnderecos);
        }

        public async Task<ActionResult> EntregarValidar(int idCliente,int idPedido, int endSelecionado){
            _repository.EntregarPedido(idPedido,endSelecionado);
            await _unit.Salvar();
            return RedirectToAction("Index",new {idCliente = idCliente});
        }

        public async Task<ActionResult> Excluir(int idCliente,int idPedido){
             _repository.Excluir(idPedido);
             await _unit.Salvar();
             return RedirectToAction("Index",new { idCliente = idCliente});
        }

        public async Task<ActionResult> FecharPedidoHome(int idPedido){
            _repository.FecharPedido(idPedido);
            await _unit.Salvar();
            return RedirectToAction("Index","Home");
        }


        public async Task<ActionResult> ExcluirIndexHome(int idPedido){
            _repository.Excluir(idPedido);
           await _unit.Salvar();
            return RedirectToAction("Index","Home");
        }   

    }
}