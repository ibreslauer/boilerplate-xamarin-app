using Boilerplate.Common.Validation.Contracts;
using System;

namespace Boilerplate.Common.Validation
{
    public class BaseValidationRule<T> : IValidationRule<T>
    {
        public virtual Func<T, bool> ValidityCondition { get; set; }
        public virtual Func<string> DynamicInvalidMessage { get; set; }
        public virtual string InvalidMessage { get; set; }
        public virtual bool Check(T value)
        {
            if (ValidityCondition != null)
            {
                return ValidityCondition(value);
            }
            throw new NotImplementedException($"{nameof(Check)} method not implemented on {GetType().Name}");
        }
    }
}
