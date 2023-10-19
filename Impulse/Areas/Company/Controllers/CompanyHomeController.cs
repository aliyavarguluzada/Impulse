using Impulse.Core.Requests;
using Impulse.Data;
using Impulse.DTOs.Vacancies;
using Impulse.Filters;
using Impulse.Models;
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

        public CompanyHomeController(ApplicationDbContext context,
                                                           IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
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
                .ToListAsync();

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


                    string userName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "Name").Select(c => c.Value).FirstOrDefault();




                    var requiredProperties = new[]
                    {
                         addRequest.CityId,
                         addRequest.EducationId,
                         addRequest.ExperienceId,
                         addRequest.JobCategoryId,
                         addRequest.JobTypeId,
                         addRequest.WorkFormId
                    };

                    if (requiredProperties.Any(value => value == null || value == 0))
                    {
                        ModelState.AddModelError(string.Empty, "Select values for all required fields.");
                        return View(addRequest);
                    }

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
                        CityId = addRequest.CityId,
                        CompanyName = userName


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
