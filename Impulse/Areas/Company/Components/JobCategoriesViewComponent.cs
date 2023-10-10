using Impulse.Data;
using Impulse.DTOs.CompanyInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Areas.Company.Components
{
    public class JobCategoriesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public JobCategoriesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var jobCategories = await _context
               .JobCategories
               .Select(c => new JobCategoryDto
               {
                   JobCategoryId = c.Id,
                   JobCategoryName = c.Name

               }).ToListAsync();

            return View(jobCategories);
        }
    }
}
