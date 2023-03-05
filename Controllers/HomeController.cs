using Microsoft.AspNetCore.Mvc;

namespace  ProjetoCurso.Controllers
{
 public class HomeController : Controller {
    public ActionResult Index(){
        return View();
    }
 }
    
}