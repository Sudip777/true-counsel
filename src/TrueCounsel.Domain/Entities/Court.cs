using System.Collections.Generic;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.Entities
{
    public class Court : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public CourtType CourtTypeField { get; set; } = null!;

        // Principal navigation required by the LegalCase configuration
        private readonly List<LegalCase> _cases = new();
        public IReadOnlyCollection<LegalCase> Cases => _cases.AsReadOnly();
    }
}