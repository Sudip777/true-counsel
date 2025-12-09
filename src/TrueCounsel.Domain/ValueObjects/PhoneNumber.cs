using System.Text.RegularExpressions;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        public string Value { get; }

        private PhoneNumber(string number)
        {
            Value = number;
        }

        public static PhoneNumber Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Phone number cannot be empty.");

            input = input.Trim();

            // Allowed:
            // 1) 10 digits only
            // 2) +XXX10digits
            // 3) +XXX-10digits

            var pattern = @"^(?:\+\d{3}-?\d{10}|\d{10})$";

            if (!Regex.IsMatch(input, pattern))
                throw new ArgumentException("Invalid phone number format.");

            return new PhoneNumber(input);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }

}
