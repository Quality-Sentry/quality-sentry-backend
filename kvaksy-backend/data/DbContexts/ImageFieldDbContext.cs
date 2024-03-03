using kvaksy_backend.data.models;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.data.DbContexts
{
    public class ImageFieldDbContext: DbContext
    {
        public DbSet<ImageField> ImageFields { get; set; }

        public ImageFieldDbContext(DbContextOptions<ImageFieldDbContext> options) : base(options)
        {
            // Check if the database exists and create it if it doesn't
            Database.EnsureCreated();
        }
    }
}
