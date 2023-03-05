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
        public async void Adicionar(CategoriaModel produto)
        {
           await _tabela.AddAsync(produto);
        }

        public void Atualizar(CategoriaModel produto)
        {
           _tabela.Entry(produto).State = EntityState.Modified;
        }

        public async Task<CategoriaModel> BuscarPorId(int id)
        {
           return await _tabela.FindAsync(id);
        }

        public async Task<List<CategoriaModel>> BuscarTodos()
        {
            return await _tabela.ToListAsync();
        }

        public void Excluir(CategoriaModel produto)
        {
            _tabela.Remove(produto);
        }
    }
}