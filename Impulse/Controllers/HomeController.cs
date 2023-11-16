using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Impulse.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [OutputCache]

        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}