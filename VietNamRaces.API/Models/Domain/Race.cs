﻿namespace VietNamRaces.API.Models.Domain
{
    public class Race
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Length { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
