using ProjetoCurso.Model;

namespace ProjetoCurso.Repositories.Interfaces {
    public interface IProdutoRepository {

        public Task<List<ProdutoModel>> BuscarTodos();

        public Task<ProdutoModel> BuscarPorId(int? id);

        public void Adicionar(ProdutoModel produto);

        public void Atualizar(ProdutoModel produto);

        public void Excluir(int id);

        public  Task<bool> VerificarExistenciaPorNome(string nome);

    }

}