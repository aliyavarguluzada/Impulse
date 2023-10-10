using Impulse.Data;
using Impulse.DTOs.SecondarySiteSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace Impulse.Components
{
    public class SecondarySettingsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SecondarySettingsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var secondarySettings = await _context
              .SecondarySiteSettings
              .Select(c => new SecondarySiteSettingsDto
              {
                  DescName = c.DescName,
                  TitleName = c.TitleName,
                  Description = c.Description,
                  ImageName = c.ImageName

              }).ToListAsync();

            return View(secondarySettings);
        }
    }

}
