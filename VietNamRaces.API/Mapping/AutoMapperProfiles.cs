using AutoMapper;
using VietNamRaces.API.Models.Domain;
using VietNamRaces.API.Models.DTO.Difficulties;
using VietNamRaces.API.Models.DTO.Races;
using VietNamRaces.API.Models.DTO.Regions;

namespace VietNamRaces.API.Mapping
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap(); 
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddRaceRequestDto, Race>().ReverseMap();
            CreateMap<Race, RaceDto>().ReverseMap();
            CreateMap<UpdateRaceRequestDto, Race>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
