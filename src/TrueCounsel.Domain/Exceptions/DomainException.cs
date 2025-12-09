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
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message) { }
        protected DomainException(string message, Exception inner) : base(message, inner) { }
        protected DomainException() : base() { }
    }
}
