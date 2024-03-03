using System.ComponentModel.DataAnnotations;

namespace kvaksy_backend.data.models
{
    public class WeightField: ReportFieldBase
    {
        [Key]
        public Guid Id { get; set; }
        public double Weight { get; set; }
    }
}
