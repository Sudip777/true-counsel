
using System;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.Entities
{
    public class CaseStatusHistory : BaseEntity  // No audit needed on history
    {
        public int CaseId { get;  set; }
        public string? OldStatus { get;  set; }
        public string NewStatus { get;  set; } = null!;
        public int ChangedByUserId { get;  set; }
        public string? Notes { get;  set; }
        public DateTime ChangedAt { get;  set; }

        public LegalCase LegalCase { get; private set; } = null!;
        public User ChangedBy { get; private set; } = null!;


       
    }
}
