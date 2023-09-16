using Impulse.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Impulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginRequest loginRequest)
        {
            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AdminRegisterRequest registerRequest)
        {
            return View();
        }

        //Bir defe register yazilmalidi ki passwordu hashlayib bazaya atasan sonra register silinmelidi
    }
}
