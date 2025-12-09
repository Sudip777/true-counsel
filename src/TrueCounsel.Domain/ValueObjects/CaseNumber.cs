using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.ValueObjects
{
    public sealed class CaseNumber : ValueObject
    {
        public string Value { get; }

        private CaseNumber(string value)
        {
            Value = value;
        }

        public static CaseNumber Create()
            => new CaseNumber($"CASE-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper(CultureInfo.InvariantCulture)}");

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }

}
