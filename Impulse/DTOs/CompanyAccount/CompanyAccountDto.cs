namespace Impulse.DTOs.CompanyAccount
{
    public class CompanyAccountDto
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }

    }
}
