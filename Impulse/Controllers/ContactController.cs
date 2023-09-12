using Impulse.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Impulse.Models;
using System.Net;

namespace Impulse.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<ContactController> _logger;

        public ContactController(ApplicationDbContext context, ILogger<ContactController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Index(string email, string subject, string description)
        {
            if (email is null)
            {
                Console.WriteLine("Email is empty");
            }

            var contactInfo = new ContactInfo
            {
                Email = email,
                Subject = subject,
                Description = description

            };

            await _context.AddAsync(contactInfo);
            await _context.SaveChangesAsync();

            return Json(new
            {
                status = HttpStatusCode.OK,
                data = contactInfo
            });
        }
    }
}
