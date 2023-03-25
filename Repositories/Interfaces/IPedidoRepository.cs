using ProjetoCurso.Model;

namespace ProjetoCurso.Repositories.Interfaces {
    public interface IPedidoRepository{
          public Task<List<PedidoModel>> BuscarTodos(int id);

          public Task<List<PedidoModel>> BuscarTodos();

         public Task<PedidoModel> BuscarPorId(int? id);

         public void Adicionar(PedidoModel pedido);

         public void Atualizar(PedidoModel pedido);

         public void Excluir(int id);

         public void FecharPedido(int id);

         public void EntregarPedido(int idPedido,int idEndereco);

    }

}