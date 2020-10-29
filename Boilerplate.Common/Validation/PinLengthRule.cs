using Boilerplate.Common.Constants;

namespace Boilerplate.Common.Validation
{
    public class PinLengthRule<T> : BaseValidationRule<T>
    {
        public override bool Check(T value)
        {
            var str = value as string;
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            return str.Length >= AppConstants.MIN_PIN_LENGTH;
        }
    }
}
