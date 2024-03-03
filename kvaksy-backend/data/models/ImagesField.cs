using System.ComponentModel.DataAnnotations;

namespace kvaksy_backend.data.models
{
    public class ImagesField: ReportFieldBase
    {
        [Key]
        public Guid Id { get; set; }
        public string Url { get; set; }
    }
}
