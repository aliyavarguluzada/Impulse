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

            return View();
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


                    Vacancy vacancy = new()
                    {
                        Name = addRequest.VacancyName,
                        Description = addRequest.Description,
                        Email = addRequest.Email,
                        StartDate = DateTime.Now,
                        ExpireDate = DateTime.Now.AddDays(30), // Hangfire



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
                catch (Exception)
                {
                    transaction.Rollback();

                }
            }

            return View();
        }
    }
}
