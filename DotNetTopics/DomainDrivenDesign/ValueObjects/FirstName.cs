using DomainDrivenDesign.Primitives;
using LanguageExt.Common;

namespace DomainDrivenDesign.ValueObjects
{
    public sealed class FirstName : ValueObject
    {
        public const int MaxLength = 50;

        public string Value { get; } // It have to be immutable by design

        private FirstName(string value)
        {
            Value = value;
        }

        public static Result<FirstName> Create(string value)
        {
            if(value is null)
            {
                return new Result<FirstName>(new ArgumentNullException(nameof(value)));
            }

            if(value.Length > MaxLength)
            {
                return new Result<FirstName>(new Exception($"The value is too long. Max length is {MaxLength}"));
            }

            return new Result<FirstName>(new FirstName(value));
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
