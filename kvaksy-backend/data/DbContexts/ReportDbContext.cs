using kvaksy_backend.data.models;
using kvaksy_backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.data.DbContexts
{
    public class ReportDbContext: DbContext
    {
        public DbSet<Report> Reports { get; set; }

        public DbSet<TemperatureField> TemperatureFields { get; set; }
        public DbSet<WeightField> WeightFields { get; set; }
        public DbSet<ImageField> ImageFields { get; set; }
        public DbSet<ReportFieldsConfiguration> ReportFieldsConfiguration { get; set; }

        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
        {
            // Check if the database exists and create it if it doesn't
            Database.EnsureCreated();
        
            // Ensure that there exists a configuration for the report fields
            try
            {
                EnsureReportFieldsConfigurationExists();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        void EnsureReportFieldsConfigurationExists()
        {
            if (!ReportFieldsConfiguration.Any())
            {
                ReportFieldsConfiguration.Add(
                    new ReportFieldsConfiguration
                    {
                        TemperatureField = true,
                        WeightField = true,
                        ImageField = true
                    }
                );
                SaveChanges();
            }
        }
    }
}
