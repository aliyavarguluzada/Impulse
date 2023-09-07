using Impulse.Data;
using Microsoft.AspNetCore.Mvc;

namespace Impulse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            var siteSettings = _context.SiteSettings.ToList();
            // Dto dan almaq lazimdi melumatlari Select ele Dto yarat
            return View(siteSettings);
        }

    }
}