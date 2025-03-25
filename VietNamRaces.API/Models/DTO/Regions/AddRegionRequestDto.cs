using System.ComponentModel.DataAnnotations;

namespace VietNamRaces.API.Models.DTO.Regions
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Code has to be minimum of 1 character")]
        [MaxLength(4, ErrorMessage = "Code has to be maximum of 4 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be maximum of 100 characters")]
        public string Name { get; set; }
        public string? RegionImageURL { get; set; }
    }
}
