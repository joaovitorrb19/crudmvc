using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProjetoCurso")));
builder.Services.AddScoped<DataContext>();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.UseEndpoints(endpoint => {
    endpoint.MapDefaultControllerRoute();
});



app.Run();
