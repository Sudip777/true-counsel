using System.Collections.Generic;
using TrueCounsel.Domain.Common;
using TrueCounsel.Domain.Enums;

namespace TrueCounsel.Domain.Entities
{
    public class Court : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public CourtType CourtTypeField { get; set; } = null!;

        // Navigation: collection of LegalCase for the .WithMany(crt => crt.LegalCases) mapping
        private readonly List<LegalCase> _legalCases = new();
        public IReadOnlyCollection<LegalCase> LegalCases => _legalCases.AsReadOnly();
    }
}

