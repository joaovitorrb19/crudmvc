using ProjetoCurso.Model;

namespace ProjetoCurso.Repositories.Interfaces {
    public interface IClienteRepository {
         public Task<List<ClienteModel>> BuscarTodos();

         public Task<ClienteModel> BuscarPorId(int? id);

         public void Adicionar(ClienteModel cliente);

         public void Atualizar(ClienteModel cliente);

         public void Excluir(int id);
    }
}