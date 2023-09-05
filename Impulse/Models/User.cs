namespace Impulse.Models
{
    public class User
    {
        public int Id { get; set; }
        public int UserStatusId { get; set; }
        //public int CompanyId { get; set; }
        public int UserRoleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public byte[] Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public UserRole UserRole { get; set; }
    }
}
