using Impulse.Data;
using Impulse.DTOs.CompanyInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Areas.Company.Components
{
    public class CitiesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CitiesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cities = await _context
                .Cities
                .Select(c => new CityDto
                {
                    CityId = c.Id,
                    CityName = c.Name

                }).ToListAsync();

            return View(cities);
        }
    }
}
