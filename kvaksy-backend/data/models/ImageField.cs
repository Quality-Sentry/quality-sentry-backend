using System.ComponentModel.DataAnnotations;

namespace kvaksy_backend.data.models
{
    public class ImageField: ReportFieldBase
    {
        [Key]
        public new Guid Id { get; set; }

        public int Amount { get; set; } = 3;
        public ICollection<ImageFieldUrl>? Urls { get; set; } = new List<ImageFieldUrl>();

        public ImageField() {
            this.Name = "images";
        }

    }

    public class ImageFieldUrl
    {
        [Key]
        public Guid Id { get; set; }
        public string Url { get; set; } = "N/A";
    }
}
