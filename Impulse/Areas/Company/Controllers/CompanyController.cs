using Microsoft.AspNetCore.Mvc;

namespace Impulse.Areas.Company.Controllers
{
    public class CompanyController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
