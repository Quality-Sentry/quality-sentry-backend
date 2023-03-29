using System.ComponentModel.DataAnnotations;

namespace kvaksy_backend.Data.Models
{
    public class ImageUrl
    {
        [Key]
        public Guid Id { get; set; }
        public string Url { get; set; }
    }

}
