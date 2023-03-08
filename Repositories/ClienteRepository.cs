using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;
using ProjetoCurso.Model;
using ProjetoCurso.Repositories.Interfaces;

namespace ProjetoCurso.Repositories {
    public class ClienteRepository : IClienteRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<ClienteModel> _tabela;

        public ClienteRepository(DataContext context)
        {
            _context = context;
            _tabela = _context.Clientes;
        }

        public async void Adicionar(ClienteModel cliente)
        {
          await _tabela.AddAsync(cliente);
        }

        public void Atualizar(ClienteModel cliente)
        {
            _tabela.Entry(cliente).State = EntityState.Modified;
        }

        public async Task<ClienteModel> BuscarPorId(int? id)
        {
            var aux = await _tabela.AsNoTracking().FirstOrDefaultAsync(x => x.ClienteId == id);
            return aux;
        }

        public async Task<List<ClienteModel>> BuscarTodos()
        {
            var aux = await _tabela.AsNoTracking().ToListAsync();
            return aux ;
        }

        public void Excluir(int id)
        {
            var aux = _tabela.AsNoTracking().FirstOrDefault(x => x.ClienteId == id);
            _tabela.Remove(aux);
        }
    }
}