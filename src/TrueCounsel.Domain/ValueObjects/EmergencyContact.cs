using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.ValueObjects
{
    public sealed class EmergencyContact : ValueObject
    {
        public string Name { get; }
        public string Phone { get; }

        public EmergencyContact(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Phone;
        }
    }

}
