using Microsoft.AspNetCore.Mvc;

namespace Impulse.Areas.Company.Controllers
{
    [Area("Company")]
    public class CompanyHomeController : Controller
    {
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
    }
}
