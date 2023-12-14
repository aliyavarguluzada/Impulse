using Impulse.Core.Requests;
using Impulse.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;


namespace Impulse.Areas.Company.Controllers
{
    [Area("Company")]
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;

        public AccountController(IHttpContextAccessor httpContextAccessor,
                                        IAccountService accountService,
                                        IAuthService authService)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
            _authService = authService;
        }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {


            if (!ModelState.IsValid)
                return View(registerRequest);

            var result = await _accountService.Register(registerRequest);

            if (result.Status != 200)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                    return View(registerRequest);
                }

            }
            return RedirectToAction("Login", "Account", new { area = "Company" });

        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest, bool isAdmin, bool isCompany)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("AddVacancy", "CompanyHome", new { area = "Company" });


            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "");
                return View();
            }
            var result = await _accountService.Login(loginRequest, false, true);

            if (result.Status != 200)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }

                return View(loginRequest);
            }

            var cookieAuthModel = new CookieAuthRequest
            {
                UserId = result.Response.UserId,
                Name = result.Response.Name,
                Email = result.Response.Email,
                RoleId = result.Response.RoleId,
                Role = result.Response.Role
            };





            var cookieAuthResult = await _authService.CookieAuth(cookieAuthModel);

            if (cookieAuthResult.Status != 200)
            {
                foreach (var item in cookieAuthResult.Errors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }

                return View(loginRequest);
            }

            return RedirectToAction("Index", "CompanyHome", new { area = "Company" });
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "default" });
        }
    }
}
