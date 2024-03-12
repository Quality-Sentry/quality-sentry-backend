using kvaksy_backend.Data.Models;

namespace kvaksy_backend.data.models
{
    public class ApiResponse
    {
        public string Message { get; set; } = "Success";
        public object Object { get; set; }
        public bool Error { get; set; } = false;

        public ApiResponse(object Object)
        {
            this.Object = Object;
        }

        public ApiResponse(string Message, object Object)
        {
            this.Message = Message;
            this.Object = Object;
        }

        public ApiResponse(string Message, object Object, bool Error)
        {
            this.Message = Message;
            this.Object = Object;
            this.Error = Error;
        }
    }
}
