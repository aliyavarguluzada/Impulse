namespace Impulse.DTOs.Cvs
{
    public record CvsDto
    {
        public int CvId { get; set; }
        public string ImageName { get; set; }
        public string FilePath { get; set; }
        public bool? MainPage { get; set; }
    }
}
