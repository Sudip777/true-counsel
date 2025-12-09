
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.ValueObjects
{
    public sealed class HourlyRate : ValueObject
    {
        public decimal Value { get; }

        private HourlyRate(decimal value)
        {
            Value = value;
        }

        public static HourlyRate Create(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("Hourly rate cannot be negative.");

            return new HourlyRate(decimal.Round(value, 2));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }

}
