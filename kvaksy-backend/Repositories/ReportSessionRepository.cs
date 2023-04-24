using kvaksy_backend.Data;
using kvaksy_backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.Repositories
{
    public interface IReportSessionRepository
    {
        List<ReportSession> GetAll();
        ReportSession? UpdateReportSession(ReportSession reportSession);
        ReportSession? GetReportSession(Guid id);
        bool CreateReportSession(ReportSession reportSession);
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
            return _dbContext.ReportSessions.Include(x => x.ImageUrls).ToList();
        }
        public ReportSession? GetReportSession(Guid id)
        {
            return _dbContext.ReportSessions.Include(x => x.ImageUrls).Include(x => x.Report).FirstOrDefault(x => x.Id == id);

        }
        public bool CreateReportSession(ReportSession reportSession)
        {
            var result = _dbContext.ReportSessions.Add(reportSession);
            if (result.State == EntityState.Added)
            {
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public ReportSession? UpdateReportSession(ReportSession reportSession)
        {
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
