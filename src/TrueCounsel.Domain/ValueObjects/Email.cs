using System.Globalization;
using System.Text.RegularExpressions;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.ValueObjects
{
    public sealed class Email:ValueObject 
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty.");

            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Invalid email format.");

            return new Email(value.Trim().ToUpperInvariant());
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }

}
