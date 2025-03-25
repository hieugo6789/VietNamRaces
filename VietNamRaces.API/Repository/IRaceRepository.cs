using VietNamRaces.API.Models.Domain;

namespace VietNamRaces.API.Repository
{
    public interface IRaceRepository
    {
        Task<Race> CreateRaceAsync(Race race);
        Task<List<Race>> GetAllRaceAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10);
        Task<Race?> GetRaceByIdAsync(Guid id);
        Task<Race?> UpdateRaceAsync(Guid id, Race race);
        Task<Race?> DeleteRaceByIdAsync(Guid id);
    }
}
