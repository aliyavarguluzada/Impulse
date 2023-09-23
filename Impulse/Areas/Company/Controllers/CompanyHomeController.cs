using Impulse.Data;
using Microsoft.AspNetCore.Mvc;

namespace Impulse.Areas.Company.Controllers
{
    [Area("Company")]
    public class CompanyHomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyHomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddVacancy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVacancy(string name)
        {
            return RedirectToAction("");
        }
    }
}
