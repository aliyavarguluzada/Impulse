using Impulse.Core.Requests;
using Impulse.Data;
using Microsoft.AspNetCore.Mvc;

namespace Impulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        public readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

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
