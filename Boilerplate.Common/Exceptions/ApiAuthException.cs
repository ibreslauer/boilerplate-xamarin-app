using Boilerplate.Common.Models;
using System.Net;

namespace Boilerplate.Common.Exceptions
{
    public class ApiAuthException: ApiRequestException
    {
        public ApiAuthException(HttpStatusCode code, ApiError apiError)
            : base(code, apiError)
        { }
    }
}
