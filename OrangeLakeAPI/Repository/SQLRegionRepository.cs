using Microsoft.EntityFrameworkCore;
using OrangeLakeAPI.Data;
using OrangeLakeAPI.Models.Domains;

namespace OrangeLakeAPI.Repository
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly OrangeLakeDbContext dbContext;
        public SQLRegionRepository(OrangeLakeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Region>> GetAllAysnc()
        {
           return await dbContext.Regions.ToListAsync();
        }
    }
}
