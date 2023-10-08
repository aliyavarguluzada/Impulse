using Impulse.Data;
using Impulse.DTOs.Cvs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Components
{
    public class CvsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CvsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cvs = await _context
                .Cvs
                .Select(c => new CvsDto
                {
                    CvId = c.Id,
                    MainPage = c.MainPage,
                    ImageName = c.ImageName
                }).OrderBy(c => c.CvId).Take(3).ToListAsync();

            return View(cvs);
        }
    }
}
