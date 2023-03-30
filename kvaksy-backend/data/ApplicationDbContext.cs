using kvaksy_backend.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ReportSession> ReportSessions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Removing unwanted columns from IdentityUser
            modelBuilder.Entity<ApplicationUser>()
                .Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.LockoutEnabled)
                .Ignore(c => c.LockoutEnd)
                .Ignore(c => c.ConcurrencyStamp)
                .Ignore(c => c.SecurityStamp)
                .Ignore(c => c.EmailConfirmed)
                .Ignore(c => c.TwoFactorEnabled);
            // .Ignore(c => c.NormalizedEmail)
            // .Ignore(c => c.NormalizedUserName);
        }

    }
}