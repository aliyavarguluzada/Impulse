using Impulse.Core;
using Impulse.Core.Requests;
using Impulse.Core.Responses;
using Impulse.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Impulse.Interfaces
{
    public class AuthService : IAuthService
    {
        private readonly HttpContext _httpContextAccessor;
        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor.HttpContext;
        }
        public async Task<ServiceResult<CookieAuthResponse>> CookieAuth(CookieAuthRequest cookieAuthRequest)
        {
            var claims = new List<Claim>
            {
                new Claim("Name",cookieAuthRequest.Name),
                new Claim("Email", cookieAuthRequest.Email),
                new Claim("UserRoleId", cookieAuthRequest.RoleId.ToString()),
                new Claim("Id", cookieAuthRequest.UserId.ToString()),
                new Claim("UserRole", cookieAuthRequest.Role)
            };


            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);


            await _httpContextAccessor.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return ServiceResult<CookieAuthResponse>.OK(new CookieAuthResponse());

        }
    }
}
