using Microsoft.AspNetCore.Mvc;

namespace Impulse.Controllers
{
    public class HomeController : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}