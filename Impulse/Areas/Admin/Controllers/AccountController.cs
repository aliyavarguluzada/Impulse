using Microsoft.AspNetCore.Mvc;

namespace Impulse.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        [Area("Admin")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
