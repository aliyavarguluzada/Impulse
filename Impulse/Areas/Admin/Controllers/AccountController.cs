using Impulse.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Impulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(AdminLoginRequest loginRequest)
        {
            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> AdminRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdminRegister(AdminRegisterRequest registerRequest)
        {
            return View();
        }

        //Bir defe register yazilmalidi ki passwordu hashlayib bazaya atasan sonra register silinmelidi
    }
}
