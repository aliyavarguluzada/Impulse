using Impulse.Data;
using Impulse.DTOs.Cvs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var catalogImages = await _context
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
