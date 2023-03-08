using ProjetoCurso.Model;

namespace ProjetoCurso.Repositories.Interfaces {
    public interface ICategoriaRepository {

        public Task<List<CategoriaModel>> BuscarTodos();

        public Task<CategoriaModel> BuscarPorId(int? id);

        public Task Adicionar(CategoriaModel categoria);

        public void Atualizar(CategoriaModel categoria);

        public void Excluir(int id);

    }

}