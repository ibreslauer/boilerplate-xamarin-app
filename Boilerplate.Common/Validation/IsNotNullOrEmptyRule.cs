namespace Boilerplate.Common.Validation
{
    public class IsNotNullOrEmptyRule<T> : BaseValidationRule<T>
    {
        public override bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value as string;
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
