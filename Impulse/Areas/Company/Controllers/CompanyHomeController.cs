using Impulse.Core.Requests;
using Impulse.Data;
using Impulse.DTOs.Vacancies;
using Impulse.Filters;
using Impulse.Interfaces;
using Impulse.Models;
using Impulse.ViewModels.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Areas.Company.Controllers
{
    [Area("Company")]
    [MyAuth("Company")]
    public class CompanyHomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IAddVacancyService _vacancyService;
        public CompanyHomeController(ApplicationDbContext context,
                                                        IHttpContextAccessor httpContextAccessor,
                                                                IConfiguration configuration,
                                                                        IAddVacancyService vacancyService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _vacancyService = vacancyService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var user = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .Where(c => c.Type == "Name").FirstOrDefault();



            var userVacancies = await _context
                .Vacancies
                .Where(c => c.CompanyName == user.Value)
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

            var count = await _context.Vacancies.Where(c => c.CompanyName == user.Value).CountAsync();

            decimal pageCount = Math.Ceiling(count / (decimal)10);

            ViewBag.Pagination = new PaginationModel
            {
                Url = _configuration["VacancyPath;privatePath"],
                Count = pageCount,
                Page = (int)pageCount
            };
            return View(userVacancies);
        }

        [HttpGet]
        public async Task<IActionResult> AddVacancy()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddVacancy(AddVacancyRequest addRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(addRequest);
            }
            var result = await _vacancyService.AddVacancy(addRequest);

            if (result.Status != 200)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                    return View(addRequest);
                }

            }

            return RedirectToAction("Index", "Home", new { area = "default" });
        }
    }
}
