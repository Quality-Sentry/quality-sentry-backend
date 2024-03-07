using kvaksy_backend.data.DbContexts;
using kvaksy_backend.data.models;

namespace kvaksy_backend.Repositories
{
    public interface ITemperatureRepository
    {
        Task<TemperatureField> UpdateTemperature (TemperatureField temperature);
    }

    public class TemperatureRepository : ITemperatureRepository
    {
        private readonly ReportDbContext _dbContext;

        public TemperatureRepository(ReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<TemperatureField> UpdateTemperature(TemperatureField temperature)
        {
            var updated = _dbContext.TemperatureFields.Update(temperature);
            _dbContext.SaveChanges();

            return Task.FromResult(updated.Entity);
        }
    }
}
