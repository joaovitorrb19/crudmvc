using ProjetoCurso.Model;

namespace ProjetoCurso.Repositories.Interfaces{
    public interface IItemPedidoRepository {
         public Task<List<ItemPedidoModel>> BuscarTodos(int id);

          public Task<List<ItemPedidoModel>> BuscarTodos();

         public Task<ItemPedidoModel> BuscarPorId(int? id);

         public void Adicionar(ItemPedidoModel itempedido);

         public void Atualizar(ItemPedidoModel itempedido);

         public void Excluir(int id);
    }
}