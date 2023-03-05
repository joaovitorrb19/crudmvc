using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;

namespace ProjetoCurso.Repositories {
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DataContext _context;
        private DbSet<ProdutoModel> _tabela;
        public ProdutoRepository(DataContext context)
        {
            _context = context;
            _tabela = _context.Produtos;
        }
        public async void Adicionar(ProdutoModel produto)
        {
           await _tabela.AddAsync(produto);
        }

        public void Atualizar(ProdutoModel produto)
        {
           _tabela.Entry(produto).State = EntityState.Modified;
        }

        public async Task<ProdutoModel> BuscarPorId(int id)
        {
           return await _tabela.FindAsync(id);
        }

        public async Task<List<ProdutoModel>> BuscarTodos()
        {
            return await _tabela.ToListAsync();
        }

        public void Excluir(ProdutoModel produto)
        {
            _tabela.Remove(produto);
        }
    }
}