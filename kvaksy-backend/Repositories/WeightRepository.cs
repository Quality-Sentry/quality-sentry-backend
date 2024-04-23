using kvaksy_backend.data.DbContexts;
using kvaksy_backend.data.models;

namespace kvaksy_backend.Repositories
{
    public interface IWeightRepository
    {
        Task<WeightField> UpdateWeight(WeightField weightField);
    }

    public class WeightRepository : IWeightRepository
    {
        private readonly ReportDbContext _dbContext;

        public WeightRepository(ReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<WeightField> UpdateWeight(WeightField weightField)
        {
            var updated = _dbContext.WeightFields.Update(weightField);
            _dbContext.SaveChanges();

            return Task.FromResult(updated.Entity);
        }
    }
}
