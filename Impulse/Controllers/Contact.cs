using Microsoft.AspNetCore.Mvc;

namespace Impulse.Controllers
{
    public class Contact : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
