using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;

namespace ProjetoCurso.Repositories {
    public class EnderecoRepository : IEnderecoRepository
    {   
        private readonly DataContext _context;
        private readonly DbSet<EnderecoModel> _tabela;
        public EnderecoRepository(DataContext context)
        {
            _context = context;
            _tabela = _context.Enderecos;
        }

        public async void Adicionar(EnderecoModel endereco)
        {
           await _tabela.AddAsync(endereco);
        }

        public void Atualizar(EnderecoModel endereco)
        {
            _tabela.Entry(endereco).State = EntityState.Modified;
        }

        public async Task<EnderecoModel> BuscarPorId(int? id)
        {
            var aux = await _tabela.AsNoTracking().FirstOrDefaultAsync(x => x.EnderecoId == id);
            return aux ;
        }

        public async Task<List<EnderecoModel>> BuscarTodos()
        {
            return await _tabela.AsNoTracking().ToListAsync();
        }

        public async void Excluir(int id)
        {
            var aux = await _tabela.AsNoTracking().FirstOrDefaultAsync(x => x.EnderecoId == id);
            _tabela.Remove(aux);
        }
    }
}