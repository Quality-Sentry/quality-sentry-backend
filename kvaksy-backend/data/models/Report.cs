﻿using kvaksy_backend.data.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace kvaksy_backend.Data.Models
{

    public class Report
    {
        [SwaggerSchema(ReadOnly = true)]
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

        public object ToJson()
        {
            var settings = new JsonSerializerSettings
            {
                Converters = new JsonConverter[] { new StringEnumConverter() },
                
                ContractResolver = new CamelCasePropertyNamesContractResolver(),                
            };

            // Create an anonymous object to merge properties from the Report class
            var reportObject = new
            {
                Id,
                Finished,
                CreatedAt,
                Fields
            };

            return reportObject;
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
