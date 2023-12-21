using Impulse.Core.Requests;
using Impulse.Data;
using Impulse.DTOs.adminPanel;
using Impulse.DTOs.Vacancies;
using Impulse.Enums;
using Impulse.Filters;
using Impulse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth("admin")]

    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;


        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(OptionsRequest request)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return View(request);
                    }

                    var objectsToCreate = new List<object>();

                    if (!string.IsNullOrEmpty(request.City))
                    {
                        var optCity = new City() { Name = request.City };
                        objectsToCreate.Add(optCity);
                    }

                    if (!string.IsNullOrEmpty(request.JobType))
                    {
                        var optJobType = new JobType() { Name = request.JobType };
                        objectsToCreate.Add(optJobType);
                    }

                    if (!string.IsNullOrEmpty(request.Education))
                    {
                        var optEducation = new Education() { Name = request.Education };
                        objectsToCreate.Add(optEducation);
                    }

                    if (!string.IsNullOrEmpty(request.WorkForm))
                    {
                        var optWorkForm = new WorkForm() { Name = request.WorkForm };
                        objectsToCreate.Add(optWorkForm);
                    }

                    if (!string.IsNullOrEmpty(request.Experience))
                    {
                        var optExperience = new Experience() { Name = request.Experience };
                        objectsToCreate.Add(optExperience);
                    }

                    if (!string.IsNullOrEmpty(request.JobCategory))
                    {
                        var optJobCategory = new JobCategory() { Name = request.JobCategory };
                        objectsToCreate.Add(optJobCategory);
                    }





                    foreach (var obj in objectsToCreate)
                    {
                        await _context.AddAsync(obj);
                    }

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return RedirectToAction("Index", "Dashboard", "Admin");

        }


        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _context
                .Users
                .OrderByDescending(c => c.Id)
                .Include(c => c.UserRole)
                .Select(c => new UserDto
                {
                    UserId = c.Id,
                    UserRoleId = c.UserRoleId,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    UserRole = c.UserRole.Name

                })
                .ToListAsync();

            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> Vacancies()
        {
            var vacancies = await _context
                .Vacancies
                .Where(c => c.StatusId == (int)StatusEnum.Deactive)
                .Select(c => new VacancyDto
                {
                    VacancyId = c.Id,
                    VacancyName = c.Name,
                    StatusId = c.StatusId,
                    Email = c.Email,
                    CompanyName = c.CompanyName
                })
                .ToListAsync();

            return View(vacancies);
        }


        [HttpPost]
        public async Task<IActionResult> Accept(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var vacancy = await _context.Vacancies.FirstOrDefaultAsync(c => c.Id == id);


            if (vacancy is null)
                return RedirectToAction("Vacancies", "Dashboard", "Admin");

            try
            {
                vacancy.StatusId = (int)StatusEnum.Active;

                _context.Update(vacancy);

                await _context.SaveChangesAsync();

                transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();

                throw new Exception();
            }

            return RedirectToAction("Vacancies", "Dashboard", "Admin");
        }

        [HttpGet]
        public IActionResult CvAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CvAdd(IFormFile files)
        {
           
            return RedirectToAction("Cv", "Dashboard", "Admin");
        }
    }
}
