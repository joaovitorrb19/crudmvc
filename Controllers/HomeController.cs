using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;

namespace  ProjetoCurso.Controllers
{
 public class HomeController : Controller {
    public async Task<ActionResult> Index([FromServices]DataContext context){
        var aux = await context.Pedidos.Include(x => x.Cliente).Where(x => x.DataFechamento == null).ToListAsync();
        return View(aux);
    }

 }
    
}