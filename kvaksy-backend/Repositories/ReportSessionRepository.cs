using kvaksy_backend.Data;
using kvaksy_backend.models;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.Repositories
{
    public interface IReportSessionRepository
    {
        List<ReportSession> GetAll();
        int CreateReportSession(ReportSession reportSession);
        ReportSession? UpdateReportSession(Guid id);
    }
    public class ReportSessionRepository : IReportSessionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReportSessionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }
        public List<ReportSession> GetAll()
        {
            return _dbContext.ReportSessions.ToList();
        }
        public int CreateReportSession(ReportSession reportSession)
        {
            _dbContext.ReportSessions.Add(reportSession);
            return _dbContext.SaveChanges();
        }

        public ReportSession? UpdateReportSession(Guid id)
        {
            var reportSession = _dbContext.ReportSessions.FirstOrDefault(x => x.Id == id);
            if (reportSession == default)
                return null;
            reportSession.Finished = true;

            var updated = _dbContext.ReportSessions.Update(reportSession);

            if (updated.State == EntityState.Modified)
            {
                _dbContext.SaveChanges();
                return reportSession;
            }
            else
                return null;
        }
    }
}
