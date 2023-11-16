using Impulse.Models;

namespace Impulse.DTOs.CompanyAccount
{
    public record CompanyAccountDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
    }
}
