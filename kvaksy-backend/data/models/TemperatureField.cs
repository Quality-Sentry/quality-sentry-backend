
using System.Runtime.CompilerServices;

namespace kvaksy_backend.data.models
{
    public class TemperatureField: ReportFieldBase
    {
        public double Temperature { get; set; } = 0;
        
        public TemperatureField() {
            this.Name = "temperature";
        }
    }
}
