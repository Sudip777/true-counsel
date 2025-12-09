using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; }

        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
        public static Money Create(decimal amount, string currency)
        {
            if (amount < 0)
                throw new ArgumentException("Money amount cannot be negative.");

            if (currency != "USD" && currency != "NPR")
                throw new ArgumentException("Invalid currency.");

            return new Money(decimal.Round(amount, 2), currency);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }

}
