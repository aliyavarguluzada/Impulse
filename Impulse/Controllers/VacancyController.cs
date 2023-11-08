using Impulse.Data;
using Impulse.DTOs.CompanyInfo;
using Impulse.DTOs.Vacancies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Impulse.ViewModels.Pagination;
using Impulse.ViewModels.Search;
using System.Net;
using Impulse.Enums;

namespace Impulse.Controllers
{
    public class VacancyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public VacancyController(ApplicationDbContext context,
                                                    IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {

            var vacancies = await _context
                .Vacancies
                .OrderByDescending(c => c.Id)
                .Include(c => c.City)
                .Include(c => c.Company)
                .Include(c => c.JobType)
                .Include(c => c.JobCategory)
                .Include(c => c.Education)
                .Include(c => c.Experience)
                .Where(c => c.StatusId == (int)StatusEnum.Active)
                .Select(c => new VacancyDto
                {
                    VacancyId = c.Id,
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


            var count = await _context.Vacancies.CountAsync();

            decimal pageCount = Math.Ceiling(count / (decimal)10);

            ViewBag.Pagination = new PaginationModel
            {
                Url = _configuration["VacancyPath:Path"],
                Count = pageCount,
                Page = (int)pageCount
            };

            return View(vacancies);
        }

        //[HttpGet]
        //public


        //[HttpGet]
        //public async Task<IActionResult> Search()
        //{
        //    var vacancies = _context
        //        .Vacancies
        //        .Include(c => c.Company)
        //        .Include(c => c.WorkForm)
        //        .Include(c => c.JobCategory)
        //        .Where(c => c.JobCategoryId)
        //    return View();
        //}



        [HttpGet]
        public async Task<IActionResult> Chosen(int id)
        {
            var vacancy = await _context
                .Vacancies
                .Include(c => c.City)
                .Include(c => c.Company)
                .Include(c => c.JobType)
                .Include(c => c.JobCategory)
                .Include(c => c.Education)
                .Include(c => c.Experience)
                .Select(c => new VacancyDto
                {
                    VacancyId = c.Id,
                    VacancyName = c.Name,
                    CityName = c.City.Name,
                    JobTypeName = c.JobType.Name,
                    JobCategoryName = c.JobType.Name,
                    EducationName = c.Education.Name,
                    ExperienceName = c.Experience.Name,
                    CompanyLogoImage = c.CompanyLogoImage,
                    CompanyName = c.CompanyName,
                    Email = c.Email,
                    StartDate = c.StartDate,
                    ExpireDate = c.ExpireDate,
                    Description = c.Description
                })
                .Where(c => c.VacancyId == id)
                .ToListAsync();

            return View(vacancy);
        }
    }
}
