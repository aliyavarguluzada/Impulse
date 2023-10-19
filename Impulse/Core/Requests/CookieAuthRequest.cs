namespace Impulse.Core.Requests
{
    public class CookieAuthRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
    }
}
