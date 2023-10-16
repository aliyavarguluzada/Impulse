using Impulse.Data;
using Impulse.DTOs.CompanyInfo;
using Impulse.DTOs.Vacancies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Controllers
{
    public class VacancyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacancyController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var vacancies = await _context
                .Vacancies
                .Include(c => c.City)
                //.Include(c => c.Companies)
                //.Include(c => c.JobTypes)
                //.Include(c => c.JobCategories)
                //.Include(c => c.Educations)
                //.Include(c => c.Experiences)
                .Select(c => new VacancyDto
                {
                    CityName = c.City.Name,
                    JobTypeName = c.JobType.Name,
                    JobCategoryName = c.JobType.Name,
                    EducationName = c.Education.Name,
                    ExperienceName = c.Experience.Name,
                    CompanyLogoImage = c.CompanyLogoImage,
                    StartDate = c.StartDate,
                    ExpireDate = c.ExpireDate
                })
                .ToListAsync();



            return View(vacancies);
        }
    }
}
