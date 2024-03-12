using kvaksy_backend.data.models;
using kvaksy_backend.Data.Models;
using kvaksy_backend.Repositories;

namespace kvaksy_backend.Services
{
    public interface IImageServices
    {
        Task<DbChanges<Report>> AddImageToReport(Guid id, IFormFile image);
    }
    public class ImageServices: IImageServices
    {
        private readonly IImageRepository _imageRepository;
        private readonly IReportSessionServices _reportServices;
        public ImageServices(IImageRepository imageRepository, IReportSessionServices reportSessionServices)
        {
            _imageRepository = imageRepository;
            _reportServices = reportSessionServices;
        }

        public async Task<DbChanges<Report>> AddImageToReport(Guid id, IFormFile image)
        {
            try
            {
                var report = _reportServices.GetReport(id);

                foreach (var field in report.Fields)
                {
                    if(field is ImageField imageField)
                    {
                        if(imageField.Urls!.Count > imageField.Amount)
                        {
                            throw new Exception("Report already have too many images attatched");
                        }

                        var updatedField = await _imageRepository.UpsertAsync(imageField.Id, image);

                        imageField = updatedField.Model;

                        var result = await _reportServices.UpdateReport(report);

                        return new DbChanges<Report>()
                        {
                            Model = result,
                            Changes = updatedField.Changes
                        };
                    }
                    throw new Exception("Did not find an image field on the given report");
                }
            }
            catch (Exception)
            {
                throw;
            }

            throw new Exception("An unexpected error happened");
        }
    }
}
