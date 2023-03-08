using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Model;

namespace ProjetoCurso.Data {
    public class DataContext : DbContext {

        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }

        public DbSet<CategoriaModel> Categorias {get;set;}

        public DbSet<ProdutoModel> Produtos {get;set;}

        public DbSet<EnderecoModel> Enderecos {get;set;}

        public DbSet<ClienteModel> Clientes {get;set;}

        public DbSet<PedidoModel> Pedidos {get;set;}
    }
}