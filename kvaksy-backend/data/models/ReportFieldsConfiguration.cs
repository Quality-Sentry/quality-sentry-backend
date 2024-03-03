using System.ComponentModel.DataAnnotations;

namespace kvaksy_backend.data.models
{
    public class ReportFieldsConfiguration
    {
        [Key]
        public int Id { get; set; }
        public bool ImageField { get; set; }
        public bool TemperatureField { get; set; }
        public bool WeightField { get; set; }
    }
}
