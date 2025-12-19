using System;
using System.Collections.Generic;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.Entities
{
    public class CourtType : BaseAuditableEntity
    {
        public string Name { get; private set; } = String.Empty;
        public string? Address { get;  set; }
        public string? City { get;  set; }
        public string? State { get;  set; }
        public required CourtType CourtTypeField { get;  set; }

        public IReadOnlyCollection<LegalCase> Cases => _cases.AsReadOnly();
        private readonly List<LegalCase> _cases = new();
      
    }
}
