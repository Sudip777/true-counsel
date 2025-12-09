using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueCounsel.Domain.Exceptions
{
    public class InvalidEmailAddressException : DomainException
    {
        public InvalidEmailAddressException()
            : base("Invalid email address.") { }

        public InvalidEmailAddressException(string email)
            : base($"'{email}' is not a valid email address.") { }

        public InvalidEmailAddressException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
