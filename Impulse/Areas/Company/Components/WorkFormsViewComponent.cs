using Impulse.Data;
using Impulse.DTOs.CompanyInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Areas.Company.Components
{
    public class WorkFormsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public WorkFormsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var workForms = await _context
              .WorkForms
              .Select(c => new WorkFormDto
              {
                  WorkFormId = c.Id,
                  WorkFormName = c.Name
              }).ToListAsync();

            return View(workForms);
        }
    }
}
