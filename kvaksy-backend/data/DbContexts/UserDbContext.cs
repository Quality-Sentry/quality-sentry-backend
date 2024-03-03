using kvaksy_backend.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace kvaksy_backend.data.DbContexts
{

    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public UserDbContext(DbContextOptions<UserDbContext> options, IConfiguration configuration) : base(options)
        {
            // Check if the database exists and create it if it doesn't
            Database.EnsureCreated();

            // Check if there is no users in the database and create an admin user if there isn't
            if (Users.Count() == 0)
            {
                Users.Add(
                    new User
                    {
                        Email = configuration.GetSection("Seeding").GetValue<string>("AdminEmail"),
                        Password = configuration.GetSection("Seeding").GetValue<string>("AdminPassword"),
                        Username = configuration.GetSection("Seeding").GetValue<string>("AdminUsername"),
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