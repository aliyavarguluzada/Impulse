﻿using Impulse.Data;
using Impulse.DTOs.CompanyAccount;
using Impulse.Enums;
using Impulse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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


        [HttpGet]
        public IActionResult Login()
        {
            //if (HttpContext.User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context
                .Users
                .Select(c => new CompanyAccountDto
                {
                    Email = c.Email

                }).FirstOrDefaultAsync();

            using (SHA256 sha256 = SHA256.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(buffer);

                if (!user.Password.SequenceEqual(hash))
                {
                    ModelState.AddModelError("", "Passwords do not match");
                    return RedirectToAction("Index", "Home");
                }
            }



            return RedirectToAction();
        }
    }
}
