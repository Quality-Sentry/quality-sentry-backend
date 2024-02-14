using kvaksy_backend.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.Data
{

    public class ApplicationDbContext : DbContext
    {
        public DbSet<ReportSession> ReportSessions { get; set; } = null!;
        public DbSet<Report> Reports { get; set; } = null!;
        public DbSet<User> ApplicationUsers { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}