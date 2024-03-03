using kvaksy_backend.data.models;

namespace kvaksy_backend.Data.Models
{
    public class ReportSession
    {
        public Guid Id { get; set; }
        public bool Finished { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid ReportId { get; set; }

        public ICollection<ReportFieldBase> Fields { get; set; }

        public ReportSession()
        {
            Id = Guid.NewGuid();
            Finished = false;
            CreatedAt = DateTime.Now;
            Fields = new List<ReportFieldBase>()
            {
                new ImagesField(),
                new TemperatureField(),
                new WeightField()
            };
        }
    }
}
