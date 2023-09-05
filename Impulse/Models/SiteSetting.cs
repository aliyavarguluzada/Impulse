namespace Impulse.Models
{
    public class SiteSetting
    {
        public int Id { get; set; }
        public string MainImage { get; set; }
        public string SecondaryImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
