namespace Impulse.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int LogTypeId { get; set; } //10 - xeta, 20 - information
        public DateTime Logged { get; set; }
        public string IP { get; set; }
        public string Address { get; set; }
    }
}
