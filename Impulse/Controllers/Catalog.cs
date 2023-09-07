using Microsoft.AspNetCore.Mvc;

namespace Impulse.Controllers
{
    public class Catalog : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
