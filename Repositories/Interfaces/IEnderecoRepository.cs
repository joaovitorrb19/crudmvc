using ProjetoCurso.Model;

namespace ProjetoCurso.Repositories.Interfaces {
    public interface IEnderecoRepository {

        public Task<List<EnderecoModel>> BuscarTodos();

        public Task<EnderecoModel> BuscarPorId(int? id);

        public void Adicionar(EnderecoModel endereco);

        public void Atualizar(EnderecoModel endereco);

        public Task Excluir(int id);

        public Task<bool> VerificarExistenciaPorCep(string cep,int id);
    }
}