using System.ComponentModel.DataAnnotations;

namespace Impulse.Core.Requests
{
    public class CvAddRequest
    {
        [Required(ErrorMessage = "Please select a file.")]
        [FileExtensions(Extensions = "jpg,jpeg,png,gif,bmp,svg,webp", ErrorMessage = "Invalid file type. Please upload an image file.")]
        public IEnumerable<IFormFile> Cvs { get; set; }
    }
}
