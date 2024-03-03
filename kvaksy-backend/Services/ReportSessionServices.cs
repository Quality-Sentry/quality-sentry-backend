using System.Text;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using kvaksy_backend.Data.Models;
using kvaksy_backend.Repositories;

namespace kvaksy_backend.Services
{
    public interface IReportSessionService
    {
        List<Report> GetAll();
        Report CreateReportSession(Report reportSession);
        List<Report> GetUnfinishedReportSessions();
        List<Report> GetFinishedReportSessions();
        Report? FinishReportSession(Guid reportId);
        Report UploadImage(Guid id, IFormFile files);
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
        public List<Report> GetAll()
        {
            return _reportSessionRepository.GetAll();
        }
        public Report? CreateReportSession(Report reportSession)
        {
            var result = _reportSessionRepository.CreateReportSession(reportSession);
            if (!result)
                return null;
            else
                return reportSession;

        }
        public List<Report> GetUnfinishedReportSessions()
        {
            return _reportSessionRepository.GetAll().Where(x => x.Finished == false).ToList();
        }

        public List<Report> GetFinishedReportSessions()
        {
            return _reportSessionRepository.GetAll().Where(x => x.Finished == true).ToList();
        }

        public Report FinishReportSession(Guid reportId)
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

        public Report UploadImage(Guid id, IFormFile files)
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