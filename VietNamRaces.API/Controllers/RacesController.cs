using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VietNamRaces.API.CustomActionFilters;
using VietNamRaces.API.Models.Domain;
using VietNamRaces.API.Models.DTO.Races;
using VietNamRaces.API.Repository;

namespace VietNamRaces.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRaceRepository raceRepository;

        public RacesController(IMapper mapper, IRaceRepository raceRepository)
        {
            this.mapper = mapper;
            this.raceRepository = raceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRaces([FromQuery] string? filterOn, [FromQuery] string? filterQuerry,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var racesDomainModel = await raceRepository.GetAllRaceAsync(filterOn, filterQuerry, sortBy, isAscending ?? true,
                pageNumber, pageSize);

            return Ok(mapper.Map<List<RaceDto>>(racesDomainModel));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRace([FromBody] AddRaceRequestDto addRaceRequestDto)
        {
                //Map DTO to Domain Model
                var raceDomainModel = mapper.Map<Race>(addRaceRequestDto);

                await raceRepository.CreateRaceAsync(raceDomainModel);

                //Map Domain model to DTO
                var race = mapper.Map<RaceDto>(raceDomainModel);
                return Ok(race);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRaceById([FromRoute] Guid id)
        {
            var raceDomainModel = await raceRepository.GetRaceByIdAsync(id);

            if(raceDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RaceDto>(raceDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRace([FromRoute] Guid id, [FromBody] UpdateRaceRequestDto updateRaceRequestDto)
        {
                var raceDomainModel = mapper.Map<Race>(updateRaceRequestDto);

                raceDomainModel = await raceRepository.UpdateRaceAsync(id, raceDomainModel);

                if (updateRaceRequestDto == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<RaceDto>(raceDomainModel));
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRace([FromRoute] Guid id)
        {
            var deletedRaceDomainModel = await raceRepository.DeleteRaceByIdAsync(id);

            if(deletedRaceDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RaceDto>(deletedRaceDomainModel));

        }
    }
}
