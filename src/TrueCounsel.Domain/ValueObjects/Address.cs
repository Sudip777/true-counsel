using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string Line1 { get; }
        public string City { get; }
        public string State { get; }

        public Address(string line1, string city, string state)
        {
            Line1 = line1;
            City = city;
            State = state;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Line1;
            yield return City;
            yield return State;
        }
    }

}
