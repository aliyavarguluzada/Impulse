namespace Impulse.Models
{
    public class Company
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public User User { get; set; }
        public List<Vacancy> Vacancies { get; set; }
    }
}
