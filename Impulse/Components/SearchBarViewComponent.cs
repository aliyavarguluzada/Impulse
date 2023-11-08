using Impulse.Data;
using Impulse.DTOs.CompanyInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Impulse.Components
{
    public class SearchBarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SearchBarViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = await _context
                .JobCategories
                .Select(c => new JobCategoryDto
                {
                    JobCategoryId = c.Id,
                    JobCategoryName = c.Name
                })
                .ToListAsync();

            ViewBag.JobCategory = category;

            return View();
        }
    }
}
