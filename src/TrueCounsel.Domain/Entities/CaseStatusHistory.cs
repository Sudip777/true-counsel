using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.Entities
{
    public class CaseStatusHistory : BaseEntity  // No audit needed on history
    {
        public int CaseId { get; private set; }
        public string? OldStatus { get; private set; }
        public string NewStatus { get; private set; } = null!;
        public int ChangedByUserId { get; private set; }
        public string? Notes { get; private set; }
        public DateTime ChangedAt { get; private set; }

        public Case Case { get; private set; } = null!;
        public User ChangedBy { get; private set; } = null!;

        private CaseStatusHistory() { }

        public CaseStatusHistory(Case @case, string? oldStatus, string newStatus, int changedById, string? notes = null)
        {
            ArgumentNullException.ThrowIfNull(@case);

            CaseId = @case.Id;
            Case = @case;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangedByUserId = changedById;
            Notes = notes;
            ChangedAt = DateTime.UtcNow;
        }
    }
}
