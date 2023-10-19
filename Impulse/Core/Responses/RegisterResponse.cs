namespace Impulse.Core.Responses
{
    public class RegisterResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public string Role { get; set; }
        public int RoleId { get; set; }
    }
}
