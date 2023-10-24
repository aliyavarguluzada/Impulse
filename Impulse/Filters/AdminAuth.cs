using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Impulse.Filters
{
    public class AdminAuth : Attribute, IAsyncAuthorizationFilter
    {

        private readonly string Role;
        public AdminAuth(string role)
        {
            Role = role;
        }


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;

            if (!isAuthenticated)
            {
                context.Result = new RedirectToActionResult("AdminLogin", "Account", new { area = "Admin" });

                return;
            }

            var roleClaim = context.HttpContext.User.Claims.Where(c => c.Type == "UserRole").FirstOrDefault();

            if (roleClaim is null)
            {
                context.Result = new RedirectToActionResult("AdminLogin", "Account", new { area = "Admin" });

                return;
            }


            bool roleCondition = roleClaim.Value.ToUpper().Equals(Role.ToUpper());

            if (!roleCondition)
            {
                context.Result = new RedirectToActionResult("AdminLogin", "Account", new { area = "Admin" });
                return;
            }
        }
    }
}
