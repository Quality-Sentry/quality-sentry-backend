using System.Text;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using kvaksy_backend.Data.Models;
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
        ReportSession UploadImage(Guid id, IFormFile files);
    }
    public class ReportSessionService : IReportSessionService
    {
        private readonly IReportSessionRepository _reportSessionRepository;
        private readonly IConfiguration _configuration;
        public ReportSessionService(IReportSessionRepository reportSessionRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _reportSessionRepository = reportSessionRepository;
        }
        public List<ReportSession> GetAll()
        {
            return _reportSessionRepository.GetAll();
        }
        public ReportSession? CreateReportSession(ReportSession reportSession)
        {
            var result = _reportSessionRepository.CreateReportSession(reportSession);
            if (!result)
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

        public ReportSession FinishReportSession(Guid reportId)
        {
            var reportSession = _reportSessionRepository.GetReportSession(reportId);
            if (reportSession == null)
                throw new Exception("Report session not found");

            reportSession.Finished = true;
            var finished = _reportSessionRepository.UpdateReportSession(reportSession);
            if (finished == null)
                throw new Exception("Failed to finish report session");
            return finished;
        }

        public ReportSession UploadImage(Guid id, IFormFile files)
        {
            var connectionString = _configuration.GetSection("Blobs").GetValue<string>("ConnectionString");

            BlobContainerClient container = new BlobContainerClient(connectionString, id.ToString());

            if (!container.Exists())
                container.Create(PublicAccessType.Blob);

            BlobClient blob = container.GetBlobClient(files.FileName);
            using (var stream = files.OpenReadStream())
            {
                blob.Upload(stream);
                if (!blob.Exists())
                    throw new Exception("Failed to upload image");
            }
            var session = _reportSessionRepository.GetReportSession(id);
            if (session == null)
                throw new Exception("Report session not found");

            session.ImageUrls.Add(
                new ImageUrl
                {
                    Url = blob.Uri.ToString()
                }
            );
            var updated = _reportSessionRepository.UpdateReportSession(session);
            if (updated == null)
                throw new Exception("Failed to update report session with image url");

            return updated;
        }
    }
}