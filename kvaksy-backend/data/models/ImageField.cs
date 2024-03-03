using System.ComponentModel.DataAnnotations;

namespace kvaksy_backend.data.models
{
    public class ImageField: ReportFieldBase
    {
        [Key]
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public ICollection<string> Urls { get; set; } = new List<string>();
    }
}
