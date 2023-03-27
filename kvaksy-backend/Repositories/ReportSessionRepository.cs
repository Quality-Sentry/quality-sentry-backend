using kvaksy_backend.Data;
using kvaksy_backend.models;

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
        }
        public List<ReportSession> GetAll()
        {
            return _dbContext.ReportSessions.ToList();
        }
    }
}
