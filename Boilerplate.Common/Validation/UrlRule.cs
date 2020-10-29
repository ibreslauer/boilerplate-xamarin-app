using System;

namespace Boilerplate.Common.Validation
{
    public class UrlRule<T> : BaseValidationRule<T>
    {
        public override bool Check(T value)
        {
            var str = value as string;
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            return Uri.TryCreate(str, UriKind.RelativeOrAbsolute, out _);
        }
    }
}
