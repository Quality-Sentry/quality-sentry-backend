using kvaksy_backend.models;
using kvaksy_backend.Repositories;

namespace kvaksy_backend.Services
{
    public interface IReportSessionService
    {
        List<ReportSession> GetAll();
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
    }
}