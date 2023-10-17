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
        public async Task<IActionResult> Index(int page = 1)
        {

            var vacancies = await _context
                .Vacancies
                .Include(c => c.City)
                .Include(c => c.Company)
                .Include(c => c.JobType)
                .Include(c => c.JobCategory)
                .Include(c => c.Education)
                .Include(c => c.Experience)
                .Select(c => new VacancyDto
                {
                    VacancyName = c.Name,
                    CityName = c.City.Name,
                    JobTypeName = c.JobType.Name,
                    JobCategoryName = c.JobType.Name,
                    EducationName = c.Education.Name,
                    ExperienceName = c.Experience.Name,
                    CompanyLogoImage = c.CompanyLogoImage,
                    CompanyName = c.CompanyName,
                    StartDate = c.StartDate,
                    ExpireDate = c.ExpireDate
                })
                .Skip((page - 1) * 10)
                .Take(10)
                .ToListAsync();

            var jobCategories = await _context
             .JobCategories
             .Select(c => new JobCategoryDto
             {
                 JobCategoryId = c.Id,
                 JobCategoryName = c.Name

             }).ToListAsync();

            var jobTypes = await _context
             .JobTypes
             .Select(c => new JobTypeDto
             {
                 JobTypeId = c.Id,
                 JobTypeName = c.Name

             }).ToListAsync();

            var workForms = await _context
              .WorkForms
              .Select(c => new WorkFormDto
              {
                  WorkFormId = c.Id,
                  WorkFormName = c.Name
              }).ToListAsync();

            ViewBag.JobTypes = jobTypes;
            ViewBag.JobCategories = jobCategories;
            ViewBag.WorkForms = workForms;

            return View(vacancies);
        }
    }
}
