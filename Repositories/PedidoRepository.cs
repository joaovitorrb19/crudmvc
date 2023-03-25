using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;

namespace ProjetoCurso {
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<PedidoModel> _tabela;

        public PedidoRepository(DataContext context)
        {
            _context = context;
            _tabela = _context.Pedidos;
        }
         public async void Adicionar(PedidoModel pedido)
        {
          await _tabela.AddAsync(pedido);
        }

        public void Atualizar(PedidoModel pedido)
        {
            _tabela.Entry(pedido).State = EntityState.Modified;
        }

        public async Task<PedidoModel> BuscarPorId(int? id)
        {
            var aux = await _tabela.AsNoTracking().FirstOrDefaultAsync(x => x.PedidoId == id);
            return aux;
        }

        public async Task<List<PedidoModel>> BuscarTodos(int id)
        {
            var aux = await _tabela.AsNoTracking().Where(x => x.ClienteId == id).ToListAsync();
            return aux ;
        }

        public async Task<List<PedidoModel>> BuscarTodos()
        {
            var aux = await _tabela.AsNoTracking().ToListAsync();
            return aux ;
        }

        public void Excluir(int id)
        {
            var aux = _tabela.AsNoTracking().FirstOrDefault(x => x.PedidoId == id);
            _tabela.Remove(aux);
        }

        public void FecharPedido(int id){
            var aux = _tabela.Find(id);
            aux.DataFechamento = DateTime.Now;
            _tabela.Entry(aux).State = EntityState.Modified;
        }   

        public void EntregarPedido(int idPedido,int idEndereco){
            var aux = _tabela.Find(idPedido);
            aux.EnderecoId = idEndereco;
            aux.DataEntrega = DateTime.Now;
            _tabela.Entry(aux).State = EntityState.Modified;
        }

    }
}