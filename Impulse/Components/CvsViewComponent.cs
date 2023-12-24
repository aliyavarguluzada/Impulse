using Impulse.Data;
using Impulse.DTOs.Cvs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Components
{
    public class CvsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public CvsViewComponent(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var count = int.Parse(_configuration["MainPage:Count"]);
            var cvs = await _context
                .Cvs
                .Where(c => c.MainPage == true)
                .Select(c => new CvsDto
                {
                    CvId = c.Id,
                    MainPage = c.MainPage,
                    ImageName = c.ImageName
                }).OrderBy(c => c.CvId).Take(count).ToListAsync();

            return View(cvs);
        }
    }
}
