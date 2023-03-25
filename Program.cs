using Microsoft.EntityFrameworkCore;
using ProjetoCurso;
using ProjetoCurso.Data;
using ProjetoCurso.Repositories;
using ProjetoCurso.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProjetoCurso")));
builder.Services.AddScoped<DataContext>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<ICategoriaRepository,CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository,ProdutoRepository>();
builder.Services.AddScoped<IClienteRepository,ClienteRepository>();
builder.Services.AddScoped<IEnderecoRepository,EnderecoRepository>();
builder.Services.AddScoped<IPedidoRepository,PedidoRepository>();
builder.Services.AddScoped<IItemPedidoRepository,ItemPedidoRepository>();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseDeveloperExceptionPage();


app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
