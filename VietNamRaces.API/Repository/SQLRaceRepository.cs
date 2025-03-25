using Microsoft.EntityFrameworkCore;
using VietNamRaces.API.Data;
using VietNamRaces.API.Models.Domain;

namespace VietNamRaces.API.Repository
{
    public class SQLRaceRepository : IRaceRepository
    {
        private readonly VietNamRaceDbContext dbContext;

        public SQLRaceRepository(VietNamRaceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Race> CreateRaceAsync(Race race)
        {
            await dbContext.Races.AddAsync(race);
            await dbContext.SaveChangesAsync();
            return race;
        }

        public async Task<Race?> DeleteRaceByIdAsync(Guid id)
        {
            var existingRace = await dbContext.Races.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRace == null)
            {
                return null;
            }

            dbContext.Races.Remove(existingRace);
            await dbContext.SaveChangesAsync();

            return existingRace;
        }

        public async Task<List<Race>> GetAllRaceAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var races = dbContext.Races.Include("Difficulty").Include("Region").AsQueryable();

            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false) 
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    races = races.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    races = isAscending ? races.OrderBy(x => x.Name) : races.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    races = isAscending ? races.OrderBy(x => x.Length): races.OrderByDescending(x => x.Length);
                }
            }

            //pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await races.Skip(skipResults).Take(pageSize).ToListAsync();
            //return await dbContext.Races.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Race?> GetRaceByIdAsync(Guid id)
        {
           return await dbContext.Races.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Race?> UpdateRaceAsync(Guid id, Race race)
        {
            var existingRace = await dbContext.Races.FirstOrDefaultAsync(x => x.Id == id);

            if(existingRace == null)
            {
                return null;
            }

            existingRace.Name = race.Name;
            existingRace.Description = race.Description;
            existingRace.Length = race.Length;
            existingRace.WalkImageUrl = race.WalkImageUrl;
            existingRace.DifficultyId = race.DifficultyId;
            existingRace.RegionId = race.RegionId;

            await dbContext.SaveChangesAsync();

            return existingRace;
        }
    }
}
