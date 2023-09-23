﻿using Impulse.Core.Requests;
using Impulse.Data;
using Impulse.Models;
using Microsoft.AspNetCore.Mvc;

namespace Impulse.Areas.Company.Controllers
{
    [Area("Company")]
    public class CompanyHomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyHomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddVacancy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVacancy(AddVacancyRequest addRequest)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return View(addRequest);

                    Vacancy vacancy = new()
                    {
                        Name = addRequest.VacancyName,
                        Description = addRequest.Description

                    };
                }
                catch (Exception)
                {
                    transaction.Rollback();

                }
            }

            return RedirectToAction("");
        }
    }
}
