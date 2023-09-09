using Impulse.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Impulse.Models;

namespace Impulse.Controllers
{
    public class Contact : Controller
    {
        private readonly ApplicationDbContext _context;

        public Contact(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string subject, string description)
        {
            if(email is null)
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
            return RedirectToAction("Index", "Contact");
        }
    }
}
