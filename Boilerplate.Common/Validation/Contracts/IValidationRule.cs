using System;

namespace Boilerplate.Common.Validation.Contracts
{
    public interface IValidationRule<T>
    {
        Func<T, bool> ValidityCondition { get; set; }
        Func<string> DynamicInvalidMessage { get; set; }
        string InvalidMessage { get; set; }
        bool Check(T value);
    }
}
