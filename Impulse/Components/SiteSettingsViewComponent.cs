using Impulse.Data;
using Impulse.DTOs.SiteSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Components
{
    public class SiteSettingsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SiteSettingsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
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

            return View(siteSettings);
        }
    }
}
