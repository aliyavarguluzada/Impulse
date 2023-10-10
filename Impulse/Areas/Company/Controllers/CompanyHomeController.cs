using Impulse.Core.Requests;
using Impulse.Data;
using Impulse.DTOs.CompanyInfo;
using Impulse.Filters;
using Impulse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Areas.Company.Controllers
{
    [Area("Company")]
    //[MyAuth("Company")]
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

            //var workForms = await _context
            //    .WorkForms
            //    .Select(c => new WorkFormDto
            //    {
            //        WorkFormId = c.Id,
            //        WorkFormName = c.Name
            //    }).ToListAsync();

            //var jobTypes = await _context
            //    .JobTypes
            //    .Select(c => new JobTypeDto
            //    {
            //        JobTypeId = c.Id,
            //        JobTypeName = c.Name

            //    }).ToListAsync();

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

            ViewBag.JobTypes = jobTypes;
            ViewBag.JobCategories = jobCategories;
            ViewBag.Experiences = experiences;
            ViewBag.WorkForms = workForms;
            ViewBag.Cities = cities;
            ViewBag.Educations = educations;


            //request.WorkForms = workForms;
            //request.JobTypes = jobTypes;
            //request.JobCategories = jobCategories;
            //request.Cities = cities;
            //request.Educations = educations;
            //request.Experiences = experiences;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddVacancy(AddVacancyRequest addRequest)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {


                    if (String.IsNullOrEmpty(addRequest.VacancyName))
                        ModelState.AddModelError("VacancyName", "The vacancy name is required.");

                    if (String.IsNullOrEmpty(addRequest.Description))
                        ModelState.AddModelError("Description", "The Description is required.");

                    if (String.IsNullOrEmpty(addRequest.Email))
                        ModelState.AddModelError("Email", "Email is required");



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

                    if (!ModelState.IsValid)
                        return View(addRequest);

                    Vacancy vacancy = new()
                    {
                        Name = addRequest.VacancyName,
                        Description = addRequest.Description,
                        Email = addRequest.Email,
                        StartDate = DateTime.Now,
                        ExpireDate = DateTime.Now.AddDays(30), // Hangfire

                        EducationId = addRequest.WorkFormId,
                        ExperienceId = addRequest.ExperienceId,
                        JobCategoryId = addRequest.JobCategoryId,
                        JobTypeId = addRequest.JobTypeId,
                        WorkFormId = addRequest.WorkFormId,
                        CityId = addRequest.CityId




                        /////

                    };

                    if (addRequest.Logo != null && addRequest.Logo.Length > 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(addRequest.Logo.FileName);
                        var filePath = Path.Combine("wwwroot/images/logos", fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await addRequest.Logo.CopyToAsync(fileStream);
                        }

                        vacancy.LogoFilePath = fileName;
                        vacancy.CompanyLogoImage = fileName;
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

            return RedirectToAction("Index", "Home", new { area = "default" });
        }
    }
}
