﻿using Impulse.Data;
using Impulse.DTOs.Cvs;
using Impulse.DTOs.SiteSettings;
using Impulse.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            var siteSettings = await _context
                .SiteSettings
                .Select(c => new SiteSettingsHomeIndexDto
                {
                    MainImage = c.MainImage,
                    Title = c.Title,
                    SecondaryImage = c.SecondaryImage,
                    Description = c.Description
                }).ToListAsync();

            var cvs = await _context
                .Cvs
                .Select(c => new CvsDto
                {
                    CvId = c.Id,
                    MainPage = c.MainPage,
                    ImageName = c.ImageName
                }).ToListAsync();

            var vm = new HomeIndexVm
            {
                siteSettings = siteSettings,
                Cvs = cvs
            };

            // Dto dan almaq lazimdi melumatlari Select ele Dto yarat
            return View(vm);
        }

    }
}