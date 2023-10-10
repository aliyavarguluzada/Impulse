using Impulse.Data;
using Impulse.DTOs.CompanyInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Areas.Company.Components
{
    public class EducationsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public EducationsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var educations = await _context
               .Educations
               .Select(c => new EducationDto
               {
                   EducationId = c.Id,
                   EducationName = c.Name

               }).ToListAsync();

            return View(educations);
        }
    }
}
