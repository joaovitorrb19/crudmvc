using ProjetoCurso.Model;

namespace ProjetoCurso.Repositories.Interfaces {
    public interface ICategoriaRepository {

        public Task<List<CategoriaModel>> BuscarTodos();

        public Task<CategoriaModel> BuscarPorId(int id);

        public void Adicionar(CategoriaModel produto);

        public void Atualizar(CategoriaModel produto);

        public void Excluir(CategoriaModel produto);

    }

}