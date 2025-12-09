using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.ValueObjects
{
    public sealed class DocumentName : ValueObject
    {
        public string Value { get; }

        private DocumentName(string value)
        {
            Value = value;
        }

        public static DocumentName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Document name cannot be empty.");

            if (value.Length > 255)
                throw new ArgumentException("Document name too long.");

            return new DocumentName(value.Trim());
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }

}
