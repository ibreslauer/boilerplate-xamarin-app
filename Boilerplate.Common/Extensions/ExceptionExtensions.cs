using Boilerplate.Common.Exceptions;
using System;

namespace Boilerplate.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetErrorMessage(this Exception ex)
        {
            switch (ex.GetType().Name)
            {
                case nameof(ApiAuthException):
                    var authEx = ex as ApiAuthException;
                    return !string.IsNullOrEmpty(authEx.Message) ?
                        authEx.Message :
                        "Authentication error";

                case nameof(ApiRequestException):
                    var reqEx = ex as ApiRequestException;
                    return reqEx.Message;

                default:
                    return !string.IsNullOrEmpty(ex.Message) ?
                        ex.Message :
                        "Error executing action";
            }
        }
    }
}
