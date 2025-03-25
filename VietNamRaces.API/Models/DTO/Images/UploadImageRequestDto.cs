using System.ComponentModel.DataAnnotations;

namespace VietNamRaces.API.Models.DTO.Images
{
    public class UploadImageRequestDto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
