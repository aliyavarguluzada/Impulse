using Microsoft.AspNetCore.Mvc;

namespace Impulse.Areas.Company.Controllers
{
    public class AccountController : Controller
    {
        [Area("Company")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
