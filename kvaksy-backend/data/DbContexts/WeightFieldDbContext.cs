using kvaksy_backend.data.models;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.data.DbContexts
{
    public class WeightFieldDbContext: DbContext
    {
        public DbSet<WeightField> WeightFields { get; set; } = null!;

        public WeightFieldDbContext(DbContextOptions<WeightFieldDbContext> options) : base(options)
        {
            // Check if the database exists and create it if it doesn't
            Database.EnsureCreated();
        }
    }
}
