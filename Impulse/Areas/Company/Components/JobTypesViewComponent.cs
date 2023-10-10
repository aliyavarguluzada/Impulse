using Impulse.Data;
using Impulse.DTOs.CompanyInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Impulse.Areas.Company.Components
{
    public class JobTypesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public JobTypesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var jobTypes = await _context
               .JobTypes
               .Select(c => new JobTypeDto
               {
                   JobTypeId = c.Id,
                   JobTypeName = c.Name

               }).ToListAsync();

            return View(jobTypes);
        }
    }
}
