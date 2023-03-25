using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;

namespace ProjetoCurso.Repositories {
    public class ItemPedidoRepository : IItemPedidoRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<ItemPedidoModel> _tabela;
        public ItemPedidoRepository(DataContext context)
        {
            _context = context;
            _tabela = _context.ItensPedidos;
        }
        public async void  Adicionar(ItemPedidoModel itempedido)
        {
           await _tabela.AddAsync(itempedido);
        }

        public void Atualizar(ItemPedidoModel itempedido)
        {
            _tabela.Entry(itempedido).State = EntityState.Modified;
        }

        public async Task<ItemPedidoModel> BuscarPorId(int? id)
        {
          var aux =  await _tabela.AsNoTracking().FirstOrDefaultAsync(x => x.ItemPedidoId == id);
          return aux;
        }

        public async Task<List<ItemPedidoModel>> BuscarTodos(int id)
        {
           throw new NotImplementedException();
        }

        public async Task<List<ItemPedidoModel>> BuscarTodos()
        {
            
             var aux = await _tabela.AsNoTracking().ToListAsync();
            return aux;

        }

        public void Excluir(int id)
        {
            var aux = _tabela.Find(id);
            _tabela.Remove(aux);
        }
    }
}