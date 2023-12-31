﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Impulse.Filters
{
    public class MyAuth : Attribute, IAsyncAuthorizationFilter
    {

        private readonly string Role;
        public MyAuth(string role)
        {
            Role = role;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

            bool isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;

            if (!isAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "Company" });

                return;
            }

            var roleClaim = context.HttpContext.User.Claims.Where(c => c.Type == "UserRole").FirstOrDefault();

            if (roleClaim is null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "Company" });

                return;
            }


            bool roleCondition = roleClaim.Value.ToUpper().Equals(Role.ToUpper());

            if (!roleCondition)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "Company" });
                return;
            }


        }

    }
}
