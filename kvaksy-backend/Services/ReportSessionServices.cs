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
        Report CreateReport();
        List<Report> GetUnfinishedReportSessions();
        List<Report> GetFinishedReportSessions();
        Report? FinishReportSession(Guid reportId);
        Report UploadImage(Guid id, IFormFile files);
    }
    public class ReportSessionService : IReportSessionService
    {
        private readonly IReportRepository _reportSessionRepository;
        private readonly IConfiguration _configuration;
        public ReportSessionService(IReportRepository reportSessionRepository, IConfiguration configuration)
        {
            _configuration = configuration;
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

        public Report FinishReportSession(Guid reportId)
        {
            var reportSession = _reportSessionRepository.GetReport(reportId);
            if (reportSession == null)
                throw new Exception("Report session not found");

            reportSession.Finished = true;
            var finished = _reportSessionRepository.UpdateReport(reportSession);
            if (finished == null)
                throw new Exception("Failed to finish report session");
            return finished;
        }

        public Report UploadImage(Guid id, IFormFile files)
        {
            throw new NotImplementedException();

            //var connectionString = _configuration.GetSection("Blobs").GetValue<string>("ConnectionString");

            //BlobContainerClient container = new BlobContainerClient(connectionString, id.ToString());

            //if (!container.Exists())
            //    container.Create(PublicAccessType.Blob);

            //BlobClient blob = container.GetBlobClient(files.FileName);
            //using (var stream = files.OpenReadStream())
            //{
            //    blob.Upload(stream);
            //    if (!blob.Exists())
            //        throw new Exception("Failed to upload image");
            //}
            //var session = _reportSessionRepository.GetReportSession(id);
            //if (session == null)
            //    throw new Exception("Report session not found");

            //session.ImageUrls.Add(
            //    new ImageUrl
            //    {
            //        Url = blob.Uri.ToString()
            //    }
            //);
            //var updated = _reportSessionRepository.UpdateReportSession(session);
            //if (updated == null)
            //    throw new Exception("Failed to update report session with image url");

            //return updated;
        }
    }
}