using kvaksy_backend.data.DbContexts;
using kvaksy_backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.Repositories
{
    public interface IReportSessionRepository
    {
        List<Report> GetAll();
        Report? UpdateReportSession(Report reportSession);
        Report? GetReportSession(Guid id);
        bool CreateReportSession(Report reportSession);
    }
    public class ReportSessionRepository : IReportSessionRepository
    {
        private readonly ReportDbContext _dbContext;
        public ReportSessionRepository(ReportDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }
        public List<Report> GetAll()
        {
            //return _dbContext.Reports.Include(x => x.ImageUrls).Include(x => x.Report).ToList();

            return _dbContext.Reports.ToList();
        }
        public Report? GetReportSession(Guid id)
        {
            //return _dbContext.ReportSessions.Include(x => x.ImageUrls).Include(x => x.Report).FirstOrDefault(x => x.Id == id);
            return _dbContext.Reports.FirstOrDefault(x => x.Id == id);
        }
        public bool CreateReportSession(Report reportSession)
        {
            var result = _dbContext.Reports.Add(reportSession);
            if (result.State == EntityState.Added)
            {
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public Report? UpdateReportSession(Report reportSession)
        {
            var updated = _dbContext.Reports.Update(reportSession);

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
