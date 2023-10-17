using Impulse.Core;
using Impulse.Core.Requests;
using Impulse.Core.Responses;
using Impulse.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Impulse.Interfaces
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var user = await _context
              .Users
              .Include(c => c.UserRole)
              .Where(c => c.Email == loginRequest.Email)
              .FirstOrDefaultAsync();


            if (user is null)
                return ServiceResult<LoginResponse>.ERROR("", "Belə bir istifadəçi yoxdur");




            using (SHA256 sha256 = SHA256.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(loginRequest.Password);
                var hash = sha256.ComputeHash(buffer);

                if (!user.Password.SequenceEqual(hash))
                {
                    return ServiceResult<LoginResponse>.ERROR("", "Şifrə yanlışdır");
                }
            }




            var claims = new List<Claim>
            {
                new Claim("Name",user.Name),
                new Claim("Email", user.Email),
                new Claim("UserRoleId", user.UserRoleId.ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("UserRole", user.UserRole.Name)

            };


            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);


            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

        }


    }
}
