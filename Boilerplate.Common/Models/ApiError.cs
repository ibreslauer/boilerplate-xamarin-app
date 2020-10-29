using Newtonsoft.Json;

namespace Boilerplate.Common.Models
{
    public class ApiError
    {
        public string Message { get; set; }
        public string InnerMessage { get; set; }

        public ApiError() { }

        public ApiError(string message)
        {
            Message = message;
        }

        public ApiError(string message, string innerMessage)
        {
            Message = message;
            InnerMessage = innerMessage;
        }

        public static string ToJsonString(string message)
        {
            var error = new ApiError(message);
            return JsonConvert.SerializeObject(error);
        }
    }
}
