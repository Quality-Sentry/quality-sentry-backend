using Newtonsoft.Json;
using System.Drawing;

namespace kvaksy_backend.models
{

    public class Report
    {
        public Guid Id { get; set; }

        public String Field1 { get; set; } = "Field1";
        public String Field2 { get; set; } = "Field2";
        public String Field3 { get; set; } = "Field3";
        public String Field4 { get; set; } = "Field4";
        public String Field5 { get; set; } = "Field5";
        public String Field6 { get; set; } = "Field6";


        public Report()
        {

        }
        public Report(String field1, String field2, String field3, String field4, String field5, String field6)
        {
            Field1 = field1;
            Field2 = field2;
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