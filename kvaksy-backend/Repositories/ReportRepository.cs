using kvaksy_backend.data.DbContexts;
using kvaksy_backend.data.models;
using kvaksy_backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.Repositories
{
    public interface IReportRepository
    {
        List<Report> GetAll();
        Report? UpdateReport(Report reportSession);
        Report? GetReport(Guid id);
        bool CreateReport(Report report);
        ReportFieldsConfiguration? GetReportFieldsConfiguration();
    }
    public class ReportRepository : IReportRepository
    {
        private readonly ReportDbContext _dbContext;
        public ReportRepository(ReportDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }
        public List<Report> GetAll()
        {
            return _dbContext.Reports
                .Include(report => report.Fields)
                .ToList();
        }
        public Report? GetReport(Guid id)
        {
            return _dbContext.Reports
                .Include(x => x.Fields)
                .FirstOrDefault(x => x.Id == id);
        }
        public ReportFieldsConfiguration? GetReportFieldsConfiguration()
        {
            return _dbContext.ReportFieldsConfiguration
                .FirstOrDefault();
        }
        public bool CreateReport(Report report)
        {
            var result = _dbContext.Reports.Add(report);
            if (result.State == EntityState.Added)
            {
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public Report? UpdateReport(Report reportSession)
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
