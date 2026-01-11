using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueCounsel.Domain.Exceptions
{
    /// <summary>
    /// Base class for all domain-specific exceptions.
    /// Use this when the exception carries business meaning, not technical failure.
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception inner) : base(message, inner) { }
        public DomainException() : base() { }
    }
}
