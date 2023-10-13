using Impulse.Core.Requests;
using Impulse.Data;
using Impulse.DTOs.CompanyAccount;
using Impulse.Enums;
using Impulse.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // TODO: interface e bax istifade et. ViewComponentlere ayir Controlleri . loglama qalib input validation ele
        public AccountController(ApplicationDbContext context,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

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

                    var emails = await _context
                        .Users
                        .Select(c => c.Email)
                        .ToListAsync();


                    if (emails.Contains(registerRequest.Email))
                    {
                        ModelState.AddModelError("Email", "Bu email artiq mövcüddur");
                        return View(registerRequest);
                    }

                    User user = new User
                    {
                        Name = registerRequest.Name,
                        Phone = registerRequest.Phone,
                        Email = registerRequest.Email,
                        UserRoleId = (int)UserRoleEnum.Company,

                    };

                    using (SHA256 sha256 = SHA256.Create())
                    {
                        var buffer = Encoding.UTF8.GetBytes(registerRequest.Password);
                        var hash = sha256.ComputeHash(buffer);

                        user.Password = hash;
                    }

                    await _context.AddAsync(user);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

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


            var user = await _context
                .Users
                .Include(c => c.UserRole)
                .Where(c => c.Email == loginRequest.Email)
                .FirstOrDefaultAsync();


            if (user is null)
            {
                ModelState.AddModelError("", "Belə bir istifadəçi yoxdur");
                return View(loginRequest);
            }



            using (SHA256 sha256 = SHA256.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(loginRequest.Password);
                var hash = sha256.ComputeHash(buffer);

                if (!user.Password.SequenceEqual(hash))
                {
                    ModelState.AddModelError("", "Şifrə yanlışdır");
                    return View(loginRequest);
                }
            }




            var claims = new List<Claim>
            {
                new Claim("Name",user.Name),
                new Claim("Email", user.Email),
                new Claim("UserRoleId", user.UserRoleId.ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("UserRole", user.UserRole.Name)

            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);


            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("AddVacancy", "CompanyHome", new { area = "Company" });
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
