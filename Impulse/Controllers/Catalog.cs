using Impulse.Data;
using Impulse.DTOs.Cvs;
using Impulse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Controllers
{
    public class Catalog : Controller
    {
        private readonly ApplicationDbContext _context;

        public Catalog(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var catalogImages = _context
                .Cvs
                .Select(c => new CvsDto
                {
                    CvId = c.Id,
                    MainPage = c.MainPage,
                    ImageName = c.ImageName
                }).ToListAsync();

            return View(catalogImages);
        }
    }
}
