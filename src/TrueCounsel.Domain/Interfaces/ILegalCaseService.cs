using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Enums;

namespace TrueCounsel.Domain.Interfaces
{
    public interface ILegalCaseService
    {
        bool CanCloseCase(LegalCase legalCase);
        void CloseCase(LegalCase legalCase, CaseOutcome outcome, string notes);
    }
}
