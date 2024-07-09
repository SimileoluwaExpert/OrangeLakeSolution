using Microsoft.EntityFrameworkCore;
using OrangeLakeAPI.Models.Domains;

namespace OrangeLakeAPI.Data
{
    public class OrangeLakeDbContext: DbContext
    {
        public OrangeLakeDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { 
        
        
        }

        public DbSet<Difficulty>  Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

    }
}
