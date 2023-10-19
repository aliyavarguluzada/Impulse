using Impulse.Core;
using Impulse.Core.Requests;
using Impulse.Core.Responses;
using Impulse.Data;
using Impulse.Enums;
using Impulse.Models;
using Microsoft.AspNetCore.Authentication;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _httpContextAccessor = contextAccessor;
        }
        public async Task<ServiceResult<LoginResponse>> Login(LoginRequest loginRequest, bool isAdmin, bool isCompany)
        {
            var user = await _context
              .Users
              .Include(c => c.UserRole)
              .Where(c => c.Email == loginRequest.Email)
              .FirstOrDefaultAsync();



            if (user is null)
                return ServiceResult<LoginResponse>.ERROR("", "Belə bir istifadəçi yoxdur");


            if (isAdmin && user.UserRoleId != (int)UserRoleEnum.Admin)
                return ServiceResult<LoginResponse>.ERROR("", "Siz admin deyilsiz.");


            if (isCompany && user.UserRoleId != (int)UserRoleEnum.Company)
                return ServiceResult<LoginResponse>.ERROR("", "İstifadəçi məlumatları yanlışdır");



            using (SHA256 sha256 = SHA256.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(loginRequest.Password);
                var hash = sha256.ComputeHash(buffer);

                if (!user.Password.SequenceEqual(hash))
                {
                    return ServiceResult<LoginResponse>.ERROR("", "Şifrə yanlışdır");
                }
            }







            var response = new LoginResponse
            {
                Name = user.Name,
                Email = user.Email,
                UserId = user.Id,
                Role = user.UserRole.Name,
                RoleId = user.UserRoleId
            };

            return ServiceResult<LoginResponse>.OK(response);
        }

        public async Task<ServiceResult<RegisterResponse>> Register(RegisterRequest registerRequest)
        {


            var emails = await _context
                        .Users
                        .Include(c => c.UserRole)
                        .Select(c => c.Email)
                        .ToListAsync();


            if (emails.Contains(registerRequest.Email))
            {
                return ServiceResult<RegisterResponse>.ERROR("", "Belə bir istifadəçi artıq mövcuddur");
            }


            User user = new User
            {
                Name = registerRequest.Name,
                Phone = registerRequest.Phone,
                Email = registerRequest.Email,
                UserRoleId = (int)UserRoleEnum.Company

            };


            using (SHA256 sha256 = SHA256.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(registerRequest.Password);
                var hash = sha256.ComputeHash(buffer);

                user.Password = hash;
            }
            var response = new RegisterResponse
            {
                Name = user.Name,
                Email = user.Email,
                UserId = user.Id,
                RoleId = user.UserRoleId,
                Role = "company"
            };
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();


            return ServiceResult<RegisterResponse>.OK(response);
        }
    }
}
