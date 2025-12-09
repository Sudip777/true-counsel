using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueCounsel.Domain.Exceptions
{
    public class NegativeAmountNotAllowedException : DomainException
    {
        public NegativeAmountNotAllowedException()
            : base("Amount cannot be negative in this context.") { }
        public NegativeAmountNotAllowedException(string message) : base(message)
        {
        }

        public NegativeAmountNotAllowedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
