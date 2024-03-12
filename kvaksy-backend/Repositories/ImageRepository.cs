using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using kvaksy_backend.data.DbContexts;
using kvaksy_backend.data.models;
using kvaksy_backend.Data.Models;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace kvaksy_backend.Repositories
{
    public interface IImageRepository
    {
        Task<DbChanges<ImageField>> UpsertAsync(Guid id, IFormFile image);
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

        public async Task<DbChanges<ImageField>> UpsertAsync(Guid id, IFormFile image)
        {
            try
            {
                using var stream = image.OpenReadStream();

                // Create a BlobClient object which will be used to create and manipulate the blob
                var blob = _container.GetBlobClient(id.ToString());

                // Upload content to the blob
                await blob.UploadAsync(stream);

                // Check if the blob exists
                if (!blob.Exists())
                {
                    throw new Exception("Failed to upload image");
                }

                var blobUrl = blob.Uri.ToString();

                // Get the current image field
                var imageField = await _reportDbContext.ImageFields.FindAsync(id);

                if (imageField == null)
                {
                    throw new Exception("Image field not found");
                }

                // Add the new url to the image field
                imageField.Urls.Add(new ImageFieldUrl(blobUrl));

                // Update the image field
                _reportDbContext.ImageFields.Update(imageField);
                var changes = await _reportDbContext.SaveChangesAsync();

                return new DbChanges<ImageField>
                {
                    Model = imageField,
                    Changes = changes
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
