using kvaksy_backend.data.models;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.data.DbContexts
{
    public class TemperatureFieldDbContext: DbContext
    {
        public DbSet<TemperatureField> TemperatureFields { get; set; } = null!;

        public TemperatureFieldDbContext(DbContextOptions<TemperatureFieldDbContext> options) : base(options)
        {
            // Check if the database exists and create it if it doesn't
            Database.EnsureCreated();
        }
    }
}
