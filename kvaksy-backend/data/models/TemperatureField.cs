using System.ComponentModel.DataAnnotations;

namespace kvaksy_backend.data.models
{
    public class TemperatureField: ReportFieldBase
    {
        [Key]
        public Guid Id { get; set; }
        public double Temperature { get; set; }
    }
}
