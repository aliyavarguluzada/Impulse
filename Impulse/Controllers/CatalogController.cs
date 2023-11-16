using Impulse.Data;
using Impulse.DTOs.Cvs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IOutputCacheStore _cache;

        public CatalogController(ApplicationDbContext context,
                                        IOutputCacheStore cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet]
        [OutputCache]
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
