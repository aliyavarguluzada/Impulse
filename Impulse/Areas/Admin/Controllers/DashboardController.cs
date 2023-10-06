using Impulse.Core.Requests;
using Impulse.Data;
using Microsoft.AspNetCore.Mvc;

namespace Impulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(OptionsRequest request)
        {

            if (!ModelState.IsValid)
            {
                return View(request);
            }





            return RedirectToAction("index");
        }
    }
}
