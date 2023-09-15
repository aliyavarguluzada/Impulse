﻿using Azure.Core;
using Impulse.Core.Requests;
using Impulse.Data;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            // TODO: Input validations elave etmek  lazimdi
            if (!ModelState.IsValid)
            {
                return View(registerRequest);
            }

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
            if (user.Email == registerRequest.Email)
            {
                return View();
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
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _context
                .Users
                .Select(c => new LoginRequest
                {
                    Email = c.Email
                    //Password = c.Password

                }).FirstOrDefaultAsync();

            using (SHA256 sha256 = SHA256.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(loginRequest.Password);
                var hash = sha256.ComputeHash(buffer);

                //if (!user.Password.SequenceEqual(hash))
                //{
                //    ModelState.AddModelError("", "Passwords do not match");
                //    return RedirectToAction("Index", "Home");
                //}
            }



            return RedirectToAction();
        }
    }
}
