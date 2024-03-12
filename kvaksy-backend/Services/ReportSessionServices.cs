using kvaksy_backend.Data.Models;
using kvaksy_backend.Repositories;

namespace kvaksy_backend.Services
{
    public interface IReportSessionServices
    {
        List<Report> GetAll();
        Report CreateReport();
        List<Report> GetUnfinishedReportSessions();
        List<Report> GetFinishedReportSessions();
        Task<Report> FinishReportSession(Guid reportId);
        Task<Report> UpdateReport(Report report);
        Report GetReport(Guid reportId);
    }
    public class ReportSessionServices : IReportSessionServices
    {
        private readonly IReportRepository _reportSessionRepository;
        public ReportSessionServices(IReportRepository reportSessionRepository)
        {
            _reportSessionRepository = reportSessionRepository;
        }
        public List<Report> GetAll()
        {
            return _reportSessionRepository.GetAll();
        }
        public Report CreateReport()
        {
            var config = _reportSessionRepository.GetReportFieldsConfiguration();
            if (config == null)
            {
                throw new Exception("Report fields configuration not found");
            }

            var report = new Report().FromConfigurations(config);

            var result = _reportSessionRepository.CreateReport(report);
            if (result)
            {
                return report;
            }
            else
            {
                throw new Exception("Failed to create report session");
            }

        }
        public List<Report> GetUnfinishedReportSessions()
        {
            return _reportSessionRepository.GetAll().Where(x => x.Finished == false).ToList();
        }

        public List<Report> GetFinishedReportSessions()
        {
            return _reportSessionRepository.GetAll().Where(x => x.Finished == true).ToList();
        }

        public async Task<Report> FinishReportSession(Guid reportId)
        {
            var reportSession = _reportSessionRepository.GetReport(reportId);
            if (reportSession == null)
                throw new Exception("Report session not found");

            reportSession.Finished = true;
            var finished = await _reportSessionRepository.UpdateReport(reportSession);

            if (finished == null)
                throw new Exception("Failed to finish report session");

            return finished;
        }

        public Report GetReport(Guid reportId)
        {
            try
            {
                var report = _reportSessionRepository.GetReport(reportId);
                return report;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Report> UpdateReport(Report report)
        {
            var result = await _reportSessionRepository.UpdateReport(report);

            return result;
        }
    }
}