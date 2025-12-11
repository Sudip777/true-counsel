using System;
using TrueCounsel.Domain.Common;
using TrueCounsel.Domain.Enums;

namespace TrueCounsel.Domain.Entities
{
    public class CaseParty : BaseAuditableEntity
    {
        public int CaseId { get; set; }
        public string Name { get; set; } = null!;
        public CasePartyRole Role { get; set; }
        public string? Organization { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }

        public LegalCase LegalCase { get; set; } = null!;
    }
}
