using kvaksy_backend.data.models;

namespace kvaksy_backend.Data.Models
{

    public class Report
    {
        public Guid Id { get; set; }
        public bool Finished { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<ReportFieldBase> Fields { get; set; }

        public Report()
        {
            Id = Guid.NewGuid();
            Finished = false;
            CreatedAt = DateTime.Now;
            Fields = new List<ReportFieldBase>();
        }

        public Report FromConfigurations(ReportFieldsConfiguration configurations)
        {
            if (configurations.ImageField)
            {
                Fields.Add(new ImageField());
            }
            if (configurations.TemperatureField)
            {
                Fields.Add(new TemperatureField());
            }
            if (configurations.WeightField)
            {
                Fields.Add(new WeightField());
            }
            return this;
        }
    }
}
