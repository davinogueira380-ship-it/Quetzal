using Microsoft.AspNetCore.Mvc;

namespace Quetzal.UI.Controllers
{
    public class HomeController : Controller
    {
        // Landing page pública do site
        public IActionResult Index()
        {
            return View();
        }

        // Página estática "Sobre" o projeto/designer
        public IActionResult Sobre()
        {
            return View();
        }
    }
}
