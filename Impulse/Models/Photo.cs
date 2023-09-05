namespace Impulse.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? MainPage { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
