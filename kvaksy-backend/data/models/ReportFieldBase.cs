using Microsoft.OpenApi.Any;
using System.ComponentModel.DataAnnotations;

namespace kvaksy_backend.data.models
{
    public enum ReportFieldType
    {
        Image,
        Temperature,
        Weight
    }

    public abstract class ReportFieldBase
    {
        [Key]
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; } 

        protected ReportFieldBase()
        {
            Id = Guid.NewGuid();
            Name = "N/A";
            Description = "N/A";
        }
    }
}
