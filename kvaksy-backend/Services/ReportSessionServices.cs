using kvaksy_backend.models;
using kvaksy_backend.Repositories;

namespace kvaksy_backend.Services
{
    public interface IReportSessionService
    {
        List<ReportSession> GetAll();
        ReportSession CreateReportSession(ReportSession reportSession);
        List<ReportSession> GetUnfinishedReportSessions();
        List<ReportSession> GetFinishedReportSessions();
        ReportSession? FinishReportSession(Guid reportId);
    }
    public class ReportSessionService : IReportSessionService
    {
        private readonly IReportSessionRepository _reportSessionRepository;
        public ReportSessionService(IReportSessionRepository reportSessionRepository)
        {
            _reportSessionRepository = reportSessionRepository;
        }
        public List<ReportSession> GetAll()
        {
            return _reportSessionRepository.GetAll();
        }
        public ReportSession? CreateReportSession(ReportSession reportSession)
        {
            var result = _reportSessionRepository.CreateReportSession(reportSession);
            if (result == 0)
                return null;
            else
                return reportSession;

        }
        public List<ReportSession> GetUnfinishedReportSessions()
        {
            return _reportSessionRepository.GetAll().Where(x => x.Finished == false).ToList();
        }

        public List<ReportSession> GetFinishedReportSessions()
        {
            return _reportSessionRepository.GetAll().Where(x => x.Finished == true).ToList();
        }

        public ReportSession? FinishReportSession(Guid reportId)
        {
            return _reportSessionRepository.UpdateReportSession(reportId);
        }

    }
}