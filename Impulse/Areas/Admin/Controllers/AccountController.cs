using Impulse.Core.Requests;
using Impulse.Data;
using Impulse.Enums;
using Impulse.Filters;
using Impulse.Interfaces;
using Impulse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Impulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;


        public AccountController(ApplicationDbContext context,
                                              IAccountService accountService,
                                              IAuthService authService)
        {
            _context = context;
            _accountService = accountService;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return View(loginRequest);


            var result = await _accountService.Login(loginRequest, true, false);

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

            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }


        [HttpGet]
        public async Task<IActionResult> AdminRegister()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> AdminRegister(AdminRegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return View(registerRequest);



            User user = new User
            {
                Name = registerRequest.Name,
                Phone = registerRequest.Phone,
                Email = registerRequest.Email,
                UserRoleId = (int)UserRoleEnum.Admin
            };

            using (SHA256 sha256 = SHA256.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(registerRequest.Password);
                var hash = sha256.ComputeHash(buffer);

                user.Password = hash;
            }

            await _context.AddAsync(user);

            await _context.SaveChangesAsync();

            return RedirectToAction("Login", "Account", new { area = "Admin" });
        }


    }
}
