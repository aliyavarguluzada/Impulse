using Impulse.Models;

namespace Impulse.DTOs.adminPanel
{
    public record UserDto
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public string Name { get; set; }
        public string UserRole { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }

    }
}
