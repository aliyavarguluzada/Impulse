using Impulse.Models;
using Microsoft.AspNetCore.Mvc;

namespace Impulse.Components
{
    public class PaginationViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(PaginationModel model)
        {
            return View(model);
        }
    }
}
