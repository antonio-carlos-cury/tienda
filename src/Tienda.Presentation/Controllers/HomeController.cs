using Microsoft.AspNetCore.Mvc;

namespace Tienda.Presentation.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
