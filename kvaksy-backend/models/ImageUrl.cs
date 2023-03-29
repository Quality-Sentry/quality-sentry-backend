using System.ComponentModel.DataAnnotations;

namespace kvaksy_backend.models
{
    public class ImageUrl
    {
        [Key]
        public Guid Id { get; set; }
        public string Url { get; set; }
    }

}
