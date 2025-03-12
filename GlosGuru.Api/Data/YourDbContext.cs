using Microsoft.EntityFrameworkCore;

namespace Glosguru.Api.Data
{
    public class YourDbContext : DbContext
    {
        public YourDbContext(DbContextOptions<YourDbContext> options)
            : base(options)
        {
        }

        // Define your DbSets here
        // public DbSet<YourEntity> YourEntities { get; set; }
    }
}
