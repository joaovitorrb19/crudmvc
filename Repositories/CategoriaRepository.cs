using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;

namespace ProjetoCurso.Repositories {
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DataContext _context;
        private DbSet<CategoriaModel> _tabela;
        public CategoriaRepository(DataContext context)
        {
            _context = context;
            _tabela = _context.Categorias;
        }
        public async Task Adicionar(CategoriaModel produto)
        {
           await _tabela.AddAsync(produto);
        }

        public void Atualizar(CategoriaModel produto)
        {
           _tabela.Entry(produto).State = EntityState.Modified;
        }

        public async Task<CategoriaModel> BuscarPorId(int? id)
        {
           return await _tabela.AsNoTracking().FirstOrDefaultAsync(x => x.CategoriaId == id);
        }

        public async Task<List<CategoriaModel>> BuscarTodos()
        {
            return await _tabela.AsNoTracking().ToListAsync();
        }

        public void Excluir(int id)
        {
           var auxCategoria =  _tabela.FirstOrDefault(x => x.CategoriaId == id);
           _tabela.Remove(auxCategoria);
        }

        public async Task<bool> VerificarExistenciaPorNome(string nome){
            var aux = await _tabela.FirstOrDefaultAsync(x => x.NomeCategoria == nome);
           return aux == null ?  false :  true ;
        }
    }
}