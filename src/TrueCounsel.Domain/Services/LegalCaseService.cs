using System;
using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Enums;
using TrueCounsel.Domain.Exceptions;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Domain.Services
{
    public class LegalCaseService : ILegalCaseService
    {
        public bool CanCloseCase(LegalCase legalCase)
        {
            if (legalCase == null) throw new ArgumentNullException(nameof(legalCase));

            // Business Rule: A case can only be closed if it is Ongoing or Pending Review
            return legalCase.Status == CaseStatus.Ongoing || legalCase.Status == CaseStatus.PendingReview;
        }

        public void CloseCase(LegalCase legalCase, CaseOutcome outcome, string notes)
        {
            if (!CanCloseCase(legalCase))
            {
                throw new DomainException($"Cannot close case with status {legalCase.Status}. Only Ongoing or Pending Review cases can be closed.");
            }

            legalCase.Status = CaseStatus.Closed;
            legalCase.Outcome = outcome;
            legalCase.OutcomeNotes = notes;
            legalCase.ClosingDate = DateTime.UtcNow;
        }
    }
}
