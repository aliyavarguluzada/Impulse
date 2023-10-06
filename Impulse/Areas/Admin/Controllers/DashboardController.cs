using Impulse.Core.Requests;
using Impulse.Data;
using Impulse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Impulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;


        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
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


                    // TODO: asagidakilari interface e yigmaq lazimdi

                    var optCity = new City()
                    {
                        Name = request.JobType
                    };

                    var optJobType = new JobType()
                    {
                        Name = request.JobType
                    };

                    var optJobCategory = new JobCategory()
                    {
                        Name = request.JobCategory
                    };

                    var optWorkForm = new WorkForm()
                    {
                        Name = request.WorkForm
                    };

                    var optEducation = new Education()
                    {
                        Name = request.Education
                    };

                    var optExperience = new Experience()
                    {
                        Name = request.Experience
                    };

                    // TODO: isdese asagidaki commentleri sil  

                    //await _context.AddAsync(optCity);
                    //await _context.AddAsync(optJobType);
                    //await _context.AddAsync(optJobCategory);
                    //await _context.AddAsync(optWorkForm);
                    //await _context.AddAsync(optEducation);
                    //await _context.AddAsync(optExperience);

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

                    return Ok("Options created successfully.");


                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return RedirectToAction("index");
        }
    }
}
