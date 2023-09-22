﻿using Azure.Core;
using Impulse.Core.Requests;
using Impulse.Data;
using Impulse.DTOs.CompanyAccount;
using Impulse.Enums;
using Impulse.Models;
using Microsoft.AspNetCore.Authorization;
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



                    User user = new User
                    {
                        Name = registerRequest.Name,
                        Phone = registerRequest.Phone,
                        Email = registerRequest.Email,
                        UserRoleId = (int)UserRoleEnum.Company
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


            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "");
                return View();
            }


            var user = await _context
                .Users
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



            return RedirectToAction("Add-Vacancy", "Home", new { area = "Company" });
        }
    }
}
