using Impulse.Core.Requests;
using Impulse.Data;
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
            AddVacancyRequest request = new();

            var workForms = await _context
                .WorkForms
                .Select(c => new WorkFormDto
                {
                    WorkFormId = c.Id,
                    WorkFormName = c.Name
                }).ToListAsync();

            var jobTypes = await _context
                .JobTypes
                .Select(c => new JobTypeDto
                {
                    JobTypeId = c.Id,
                    JobTypeName = c.Name

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

                }).ToListAsync();


            var educations = await _context
                .Educations
                .Select(c => new EducationDto
                {
                    EducationId = c.Id,
                    EducationName = c.Name

                }).ToListAsync();

            var experiences = await _context
               .Experiences
                .Select(c => new ExperienceDto
                {
                    ExperienceId = c.Id,
                    ExperienceName = c.Name

                }).ToListAsync();


            request.WorkForms = workForms;
            request.JobTypes = jobTypes;
            request.JobCategories = jobCategories;
            request.Cities = cities;
            request.Educations = educations;
            request.Experiences = experiences;

            return View(request);
        }

        //
        //
        //  TempData ya yig bazadaki infolari
        //
        //
        [HttpPost]
        public async Task<IActionResult> AddVacancy(AddVacancyRequest addRequest)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //if (!ModelState.IsValid)
                    //    return View(addRequest);

                    //if (addRequest.Cities == null || addRequest.Cities.Count == 0 ||
                    //    addRequest.Educations == null || addRequest.Educations.Count == 0 ||
                    //    addRequest.Experiences == null || addRequest.Experiences.Count == 0 ||
                    //    addRequest.JobCategories == null || addRequest.JobCategories.Count == 0 ||
                    //    addRequest.JobTypes == null || addRequest.JobTypes.Count == 0 ||
                    //    addRequest.WorkForms == null || addRequest.WorkForms.Count == 0)
                    //{
                    //    ModelState.AddModelError(string.Empty, "Select values for all required fields.");
                    //    return View(addRequest);
                    //}

                    Vacancy vacancy = new()
                    {
                        Name = addRequest.VacancyName,
                        Description = addRequest.Description,
                        Email = addRequest.Email,
                        StartDate = DateTime.Now,
                        ExpireDate = DateTime.Now.AddDays(30), // Hangfire
                        CityId = addRequest.Cities.First().CityId,
                        EducationId = addRequest.Educations.First().EducationId,
                        ExperienceId = addRequest.Experiences.First().ExperienceId,
                        JobCategoryId = addRequest.JobCategories.First().JobCategoryId,
                        JobTypeId = addRequest.JobTypes.First().JobTypeId,
                        WorkFormId = addRequest.WorkForms.First().WorkFormId

                    };

                    if (addRequest.Logo != null && addRequest.Logo.Length > 0)
                    {
                        // Save the file to a location (e.g., wwwroot/images/logos)
                        var fileName = Guid.NewGuid() + Path.GetExtension(addRequest.Logo.FileName);
                        var filePath = Path.Combine("wwwroot/images/logos", fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await addRequest.Logo.CopyToAsync(fileStream);
                        }

                        // Set the logo file path in the vacancy model
                        vacancy.LogoFilePath = filePath;
                    }

                    await _context.AddAsync(vacancy);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (NullReferenceException)
                {
                    transaction.Rollback();
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
