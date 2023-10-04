using Microsoft.AspNetCore.Mvc;

namespace Impulse.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Area("Admin")]

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
