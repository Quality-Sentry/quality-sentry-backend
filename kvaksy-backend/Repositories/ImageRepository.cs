using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using kvaksy_backend.data.DbContexts;
using kvaksy_backend.Data.Models;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace kvaksy_backend.Repositories
{
    public interface IImageRepository
    {
        Task<string> UpsertAsync(Report report, IFormFile image);
        Task GetOneAsync(Guid id);
        Task<List<object>> GetAllAsync();
        Task DeletAsync(Guid id);
    }

    public class ImageRepository: IImageRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly BlobContainerClient _container;
        private readonly ReportDbContext _reportDbContext;
        public ImageRepository(IConfiguration configuration, ReportDbContext reportDbContext)
        {
            if(configuration == null)
            {
                throw new Exception("Configuration was null in ImageRepository constructor");
            }

            _connectionString = _configuration!.GetSection("Blobs").GetValue<string>("ConnectionString");
            _container = new BlobContainerClient(_connectionString, "reportImages");
            _reportDbContext = reportDbContext;

            if (!_container.Exists())
            {
                _container.Create(PublicAccessType.Blob);
            }
        }

        public Task DeletAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<object>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpsertAsync(Report report, IFormFile image)
        {
            using (var stream = image.OpenReadStream())
            {
                var blob = _container.GetBlobClient(report.Id.ToString());

                await blob.UploadAsync(stream);
                
                if (!blob.Exists())
                {
                    throw new Exception("Failed to upload image");
                }

                return blob.Uri.ToString();
            }
        }
    }
}
