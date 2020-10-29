using Boilerplate.Common.Models;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace Boilerplate.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string searchString, StringComparison comparison)
        {
            return source?.IndexOf(searchString, comparison) >= 0;
        }

        public static string ToAbsoluteUrl(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return source;

            source = !source.StartsWith("http", StringComparison.OrdinalIgnoreCase) ?
                $"http://{source}" :
                source;

            source = !source.EndsWith("/") ?
                $"{source}/" :
                source;

            return source;
        }

        public static string ToRelativeUrl(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return source;

            source = source.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ?
                source.Replace("http://", string.Empty) :
                source;

            source = source.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ?
                source.Replace("https://", string.Empty) :
                source;

            return source;
        }

        public static ApiError ToApiError(this string source)
        {
            try
            {
                return JsonConvert.DeserializeObject<ApiError>(source);
            }
            catch
            {
                return null;
            }
        }

        public static string ToSpaceDelimitedPascal(this object source)
        {
            try
            {
                return Regex.Replace(source.ToString(), "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
