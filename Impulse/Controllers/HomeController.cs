using Impulse.Data;
using Impulse.DTOs.Cvs;
using Impulse.DTOs.SecondarySiteSettings;
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
                }).Take(3).ToListAsync();

            var secondarySettings = await _context
                .SecondarySiteSettings
                .Select(c => new SecondarySiteSettingsDto
                {
                    DescName = c.DescName,
                    TitleName = c.TitleName,
                    Description = c.Description
                }).ToListAsync();

            var vm = new HomeIndexVm
            {
                siteSettings = siteSettings,
                Cvs = cvs,
                SecondarySiteSettings = secondarySettings
            };

            // Dto dan almaq lazimdi melumatlari Select ele Dto yarat
            return View(vm);
        }

    }
}