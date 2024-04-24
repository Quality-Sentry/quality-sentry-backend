
namespace kvaksy_backend.data.models
{
    public class WeightField: ReportFieldBase
    {
        public double Weight { get; set; } = 0;

        public WeightField()
        {
            this.Name = "weight";
        }
    }
}
