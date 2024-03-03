using Humanizer.Configuration;
using kvaksy_backend.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace kvaksy_backend.data.DbContexts
{

    public class UserDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;

            // Check if the database exists and create it if it doesn't
            Database.EnsureCreated();

            // Check if there is no users in the database and create an admin user if there isn't
            try
            {
                EnsureAdminUserExists();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void EnsureAdminUserExists()
        {
            if (!Users.Any())
            {
                Users.Add(
                    new User
                    {
                        Email = _configuration.GetSection("Seeding").GetValue<string>("AdminEmail"),
                        Password = _configuration.GetSection("Seeding").GetValue<string>("AdminPassword"),
                        Username = _configuration.GetSection("Seeding").GetValue<string>("AdminUsername"),
                        Role = Role.Admin,
                        FirstName = "God",
                        LastName = "Adminson"
                    }
                );
                SaveChanges();
            };
        }
    }
}