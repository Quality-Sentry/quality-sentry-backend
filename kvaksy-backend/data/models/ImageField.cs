using System.ComponentModel.DataAnnotations;

namespace kvaksy_backend.data.models
{
    public class ImageField: ReportFieldBase
    {
        [Key]
        public new Guid Id { get; set; } = Guid.NewGuid();

        public int? Amount { get; set; }
        public ICollection<ImageFieldUrl>? Urls { get; set; }


    }

    public class ImageFieldUrl
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Url { get; set; } = "N/A";

        public ImageFieldUrl(string url)
        {
            Url = url;
        }

        public ImageFieldUrl(Guid id, string url)
        {
            Id = id;
            Url = url;
        }
    }
}
