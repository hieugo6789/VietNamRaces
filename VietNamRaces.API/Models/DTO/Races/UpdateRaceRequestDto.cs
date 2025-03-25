using System.ComponentModel.DataAnnotations;

namespace VietNamRaces.API.Models.DTO.Races
{
    public class UpdateRaceRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be maximum of 100 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Name has to be maximum of 1000 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double Length { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
