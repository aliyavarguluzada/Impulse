﻿using Impulse.ViewModels.Company;
using Microsoft.AspNetCore.Mvc;

namespace Impulse.Components
{
    public class PaginationViewComponent : ViewComponent
    {
        public PaginationViewComponent() { }

        public async Task<IViewComponentResult> InvokeAsync(PaginationModel model)
        {
            return View(model);
        }
    }
}
