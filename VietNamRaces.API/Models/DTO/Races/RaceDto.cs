using VietNamRaces.API.Models.DTO.Difficulties;
using VietNamRaces.API.Models.DTO.Regions;

namespace VietNamRaces.API.Models.DTO.Races
{
    public class RaceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Length { get; set; }
        public string? WalkImageUrl { get; set; }

        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
