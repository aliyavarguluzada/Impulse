using Impulse.Core.Requests;
using Impulse.Data;
using Impulse.Enums;
using Impulse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "");
                return View();
            }

            var user = await _context
                .Users
                .Where(c => c.Email == loginRequest.Email && c.UserRoleId == (int)UserRoleEnum.Admin)
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

        //Bir defe register yazilmalidi ki passwordu hashlayib bazaya atasan sonra register silinmelidi
    }
}
