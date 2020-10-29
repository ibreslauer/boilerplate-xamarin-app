using System.Collections.Generic;
using System.Linq;

using Boilerplate.Common.Base;
using Boilerplate.Common.Validation.Contracts;

namespace Boilerplate.Common.Validation
{
    public class ValidatableObject<T> : BindableBase, IValidity
    {
        private readonly List<IValidationRule<T>> _validations;
        private List<string> _errors;
        private T _value;
        private bool _isValid;

        public List<IValidationRule<T>> Validations => _validations;

        public List<string> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                OnPropertyChanged(nameof(Errors));
            }
        }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations
                .Where(v => !v.Check(Value))
                .Select(v => v.DynamicInvalidMessage != null ? 
                    v.DynamicInvalidMessage() : 
                    v.InvalidMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return IsValid;
        }
    }
}
