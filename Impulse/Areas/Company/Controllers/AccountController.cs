using Microsoft.AspNetCore.Mvc;

namespace Impulse.Areas.Company.Controllers
{
    [Area("Company")]
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
