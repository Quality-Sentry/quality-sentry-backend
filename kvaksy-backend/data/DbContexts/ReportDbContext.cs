using kvaksy_backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.data.DbContexts
{
    public class ReportDbContext: DbContext
    {
        public DbSet<Report> Reports { get; set; } = null!;

        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
        {
            // Check if the database exists and create it if it doesn't
            Database.EnsureCreated();
        }
    }
}
