using Boilerplate.Common.Models;
using System.Net;
using System.Net.Http;

namespace Boilerplate.Common.Exceptions
{
    public class ApiRequestException : HttpRequestException
    {
        public HttpStatusCode HttpCode { get; private set; }
        public string InnerMessage { get; private set; }

        public ApiRequestException(HttpStatusCode code, ApiError apiError)
            : this(code, apiError?.Message, apiError?.InnerMessage)
        {
        }

        public ApiRequestException(HttpStatusCode code, string message, string innerMessage = null) : base(message)
        {
            HttpCode = code;
            InnerMessage = innerMessage;
        }
    }
}
