using Microsoft.AspNetCore.Mvc;

namespace Impulse.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
