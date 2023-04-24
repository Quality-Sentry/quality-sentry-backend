using Newtonsoft.Json;
using System.Drawing;

namespace kvaksy_backend.Data.Models
{

    public class Report
    {
        public Guid Id { get; set; }

        public String Temperature { get; set; } = "30";
        public String Weight { get; set; } = "2500";
        public String Field3 { get; set; } = "Field3";
        public String Field4 { get; set; } = "Field4";
        public String Field5 { get; set; } = "Field5";
        public String Field6 { get; set; } = "Field6";


        public Report()
        {

        }
        public Report(String field1, String field2, String field3, String field4, String field5, String field6)
        {
            Temperature = field1;
            Weight = field2;
            Field3 = field3;
            Field4 = field4;
            Field5 = field5;
            Field6 = field6;
        }

        public String ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Report? FromJson(String json)
        {
            return JsonConvert.DeserializeObject<Report>(json);
        }

    }
}