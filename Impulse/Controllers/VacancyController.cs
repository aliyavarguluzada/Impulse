using Impulse.Data;
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
                .Include(c => c.Company)
                .Include(c => c.Education)
                .Include(c => c.Experience)
                .Include(c => c.JobType)
                .Include(c => c.JobCategory)
                .Include(c => c.WorkForm)
                .Include(c => c.City)
                .Select(c => new VacancyDto
                {
                    VacancyId = c.Id,
                    VacancyName = c.Name,
                    CityId = c.CityId,
                    CompanyId = c.CompanyId,
                    EducationId = c.EducationId,
                    ExperienceId = c.ExperienceId,
                    Email = c.Email,
                    CompanyLogoImage = c.CompanyLogoImage,
                    Description = c.Description,
                    WorkFormId = c.WorkFormId,
                    JobCategoryId = c.JobCategoryId,
                    JobTypeId = c.JobTypeId,
                    StartDate = c.StartDate,
                    ExpireDate = c.ExpireDate


                }).ToListAsync();


            return View(vacancies);
        }
    }
}
