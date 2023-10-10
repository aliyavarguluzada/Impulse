using Impulse.Data;
using Impulse.DTOs.Cvs;
using Impulse.DTOs.SecondarySiteSettings;
using Impulse.DTOs.SiteSettings;
using Impulse.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Controllers
{
    public class HomeController : Controller
    {

        public async Task<IActionResult> Index()
        {


            return View();
        }

    }
}