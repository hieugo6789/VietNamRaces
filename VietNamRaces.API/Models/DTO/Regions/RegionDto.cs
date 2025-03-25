namespace VietNamRaces.API.Models.DTO.Regions
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageURL { get; set; }
    }
}
