using Impulse.Data;
using Impulse.DTOs.CompanyInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Areas.Company.Components
{
    public class ExperiencesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ExperiencesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var experiences = await _context
              .Experiences
               .Select(c => new ExperienceDto
               {
                   ExperienceId = c.Id,
                   ExperienceName = c.Name

               }).ToListAsync();

            return View(experiences);
        }
    }
}
