namespace kvaksy_backend.Data
{
    using kvaksy_backend.models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<ReportSession> ReportSessions { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}