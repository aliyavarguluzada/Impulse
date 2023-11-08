using Impulse.Data;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Impulse.Components
{
    public class SearchBarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SearchBarViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
