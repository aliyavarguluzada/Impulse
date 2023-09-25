using Impulse.Core.Requests;
using Impulse.Data;
using Impulse.DTOs.CompanyAccount;
using Impulse.DTOs.CompanyInfo;
using Impulse.Models;
using Impulse.ViewModels.Company;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Areas.Company.Controllers
{
    [Area("Company")]
    public class CompanyHomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyHomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddVacancy()
        {
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

            var educations = await _context
                .Educations
                .Select(c => new EducationDto
                {
                    EducationId = c.Id,
                    EducationName = c.Name
                }).ToListAsync();

            var jobCategories = await _context
                .JobCategories
                .Select(c => new JobCategoryDto
                {
                    JobCategoryId = c.Id,
                    JobCategoryName = c.Name

                }).ToListAsync();

            var cities = await _context
                        .Cities
                        .Select(c => new CityDto
                        {
                            CityId = c.Id,
                            CityName = c.Name
                        }).
                        ToListAsync();

            var experiences = await _context
                .Experiences
                .Select(c => new ExperienceDto
                {
                    ExperienceId = c.Id,
                    ExperienceName = c.Name
                }).ToListAsync();

            CompanyInfoVm vm = new()
            {
                workForms = workForms,
                educations = educations,
                jobCategories = jobCategories,
                jobTypes = jobTypes,
                cities = cities,
                experiences = experiences
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddVacancy(AddVacancyRequest addRequest)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return View(addRequest);

                    //var jobTypes = await _context
                    //    .JobTypes
                    //    .Select(c => new JobTypeDto
                    //    {
                    //        JobTypeId = c.Id,
                    //        JobTypeName = c.Name

                    //    }).ToListAsync();

                    //var workForms = await _context
                    //    .WorkForms
                    //    .Select(c => new WorkFormDto
                    //    {
                    //        WorkFormId = c.Id,
                    //        WorkFormName = c.Name

                    //    }).ToListAsync();

                    //var educations = await _context
                    //    .Educations
                    //    .Select(c => new EducationDto
                    //    {
                    //        EducationId = c.Id,
                    //        EducationName = c.Name
                    //    }).ToListAsync();

                    //var jobCategories = await _context
                    //    .JobCategories
                    //    .Select(c => new JobCategoryDto
                    //    {
                    //        JobCategoryId = c.Id,
                    //        JobCategoryName = c.Name

                    //    }).ToListAsync();

                    //var cities = await _context
                    //    .Cities
                    //    .Select(c => new CityDto
                    //    {
                    //        CityId = c.Id,
                    //        CityName = c.Name
                    //    }).
                    //    ToListAsync();
                    //var experiences = await _context
                    //                .Experiences
                    //                 .Select(c => new ExperienceDto
                    //                 {
                    //                     ExperienceId = c.Id,
                    //                     ExperienceName = c.Name

                    //                 }).ToListAsync();

                    Vacancy vacancy = new()
                    {
                        Name = addRequest.VacancyName,
                        Description = addRequest.Description,
                        Email = addRequest.Email,
                        StartDate = DateTime.Now,
                        ExpireDate = DateTime.Now.AddDays(30) // Hangfire


                    };
                    await _context.AddAsync(vacancy);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    transaction.Rollback();

                }
            }

            return View();
        }
    }
}
