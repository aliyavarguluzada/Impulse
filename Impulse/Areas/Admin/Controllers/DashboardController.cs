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

                    var optCity = new City() { Name = request.City };

                    var optJobType = new JobType() { Name = request.JobType };

                    var optJobCategory = new JobCategory() { Name = request.JobCategory };

                    var optWorkForm = new WorkForm() { Name = request.WorkForm };

                    var optEducation = new Education() { Name = request.Education };

                    var optExperience = new Experience() { Name = request.Experience };



                    var objectsToCreate = new object[] { optCity,
                                                            optJobType,
                                                                 optJobCategory,
                                                                     optWorkForm,
                                                                        optEducation,
                                                                            optExperience };

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
        public async Task<IActionResult> VacanciesA(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();


            var vacancies = await
            _context
            .Vacancies
            .Select(c => new VacancyDto
            {
                VacancyId = c.Id,
                VacancyName = c.Name,
                Email = c.Email,
                CompanyName = c.CompanyName

            })
            .Where(c => c.StatusId == (int)StatusEnum.Deactive)
            .ToListAsync();

            try
            {


                //await _context.Vacancies.AddAsync(vacancy);

                await _context.SaveChangesAsync();

                transaction.CommitAsync();

            }
            catch (Exception)
            {
                throw new Exception();
            }

            return RedirectToAction("Vacancies", "Dashboard", "Admin");
        }

        public void ChangeStatus(int id)
        {

        }
    }
}
