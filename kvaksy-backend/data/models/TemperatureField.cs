using System.ComponentModel.DataAnnotations;

namespace kvaksy_backend.data.models
{
    public class TemperatureField: ReportFieldBase
    {
        public double? Temperature { get; set; }
    }
}
