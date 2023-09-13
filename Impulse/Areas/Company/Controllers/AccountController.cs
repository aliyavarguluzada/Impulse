using Impulse.Data;
using Impulse.DTOs.CompanyAccount;
using Impulse.Enums;
using Impulse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Impulse.Areas.Company.Controllers
{
    [Area("Company")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;


        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View(new CompanyAccountDto());
        }

        [HttpPost]
        public async Task<IActionResult> Register(CompanyAccountDto dto)
        {
            // TODO: Input validations elave etmek  lazimdi

            User user = new User
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                UserRoleId = (int)UserRoleEnum.Company
            };

            using (SHA256 sha256 = SHA256.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(dto.Password);
                var hash = sha256.ComputeHash(buffer);

                user.Password = hash;
            }

            _context.AddAsync(user);

            _context.SaveChanges();

            return RedirectToAction("Login", "Account", new { area = "Company" });
        }
    }
}
