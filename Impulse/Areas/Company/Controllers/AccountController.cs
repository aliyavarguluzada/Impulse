using Impulse.Core;
using Impulse.Core.Requests;
using Impulse.Data;
using Impulse.DTOs.CompanyAccount;
using Impulse.Enums;
using Impulse.Interfaces;
using Impulse.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Impulse.Areas.Company.Controllers
{
    [Area("Company")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;

        public AccountController(ApplicationDbContext context,
                                    IHttpContextAccessor httpContextAccessor,
                                        IAccountService accountService,
                                        IAuthService authService)
        {
            _context = context;
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
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
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

                        //var emails = await _context
                        //    .Users
                        //    .Select(c => c.Email)
                        //    .ToListAsync();


                        //if (emails.Contains(registerRequest.Email))
                        //{
                        //    ModelState.AddModelError("Email", "Bu emailə bağlı bir istifadəçi artiq mövcüddur");
                        //    return View(registerRequest);
                        //}


                        //User user = new User
                        //{
                        //    Name = registerRequest.Name,
                        //    Phone = registerRequest.Phone,
                        //    Email = registerRequest.Email,
                        //    UserRoleId = (int)UserRoleEnum.Company,

                        //};


                        //using (SHA256 sha256 = SHA256.Create())
                        //{
                        //    var buffer = Encoding.UTF8.GetBytes(registerRequest.Password);
                        //    var hash = sha256.ComputeHash(buffer);

                        //    user.Password = hash;
                        //}

                        //await _context.AddAsync(user);
                        //await _context.SaveChangesAsync();

                        await transaction.CommitAsync();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
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
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("AddVacancy", "CompanyHome", new { area = "Company" });
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "");
                return View();
            }
            var result = await _accountService.Login(loginRequest);

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
