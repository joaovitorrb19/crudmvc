using ProjetoCurso.Model;

namespace ProjetoCurso.Repositories.Interfaces {
    public interface IEnderecoRepository {

        public Task<List<EnderecoModel>> BuscarTodos();

        public Task<EnderecoModel> BuscarPorId(int? id);

        public void Adicionar(EnderecoModel endereco);

        public void Atualizar(EnderecoModel endereco);

        public void Excluir(int id);
    }
}