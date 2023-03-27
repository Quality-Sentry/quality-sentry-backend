using kvaksy_backend.Data;
using kvaksy_backend.models;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.Repositories
{
    public interface IReportSessionRepository
    {
        List<ReportSession> GetAll();
    }
    public class ReportSessionRepository : IReportSessionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReportSessionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
            _dbContext.Database.Migrate();
        }
        public List<ReportSession> GetAll()
        {
            return _dbContext.ReportSessions.ToList();
        }
    }
}
