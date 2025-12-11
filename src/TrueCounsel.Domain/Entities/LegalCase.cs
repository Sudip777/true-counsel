
using TrueCounsel.Domain.Common;
using TrueCounsel.Domain.Enums;
namespace TrueCounsel.Domain.Entities
{
    public class LegalCase : BaseAuditableEntity
    {
        public required string CaseNumber { get;  set; }       
        public string? CourtCaseNumber { get;  set; }

        public int ClientId { get;  set; }
        public int LawyerId { get;  set; }
        public int? CaseCategoryId { get;  set; }
        public int? CourtId { get;  set; }
        public int CaseTypeId { get; set; }

        public string Title { get;  set; } = null!;
        public string? Description { get;  set; }
        public CaseStatus Status { get;  set; } = CaseStatus.New;
        public CasePriority Priority { get;  set; } = CasePriority.Medium;
        public DateTime? FilingDate { get;  set; }
        public DateTime? ClosingDate { get;  set; }
        public CaseOutcome? Outcome { get;  set; }
        public string? OutcomeNotes { get;  set; }
        public string Tags { get; set; } = String.Empty;

        public decimal EstimatedValue { get;  set; }

        // Navigation properties
        public Client Client { get;  set; } = null!;
        public Lawyer Lawyer { get;  set; } = null!;
        public CaseCategory? CaseCategory { get;  set; }
        public Court? Court { get;  set; }
        public CaseType? CaseType { get; set; }


        public IReadOnlyCollection<CaseParty> Parties => _parties.AsReadOnly();
        public IReadOnlyCollection<CaseNote> Notes => _notes.AsReadOnly();
        public IReadOnlyCollection<CaseStatusHistory> StatusHistory => _statusHistory.AsReadOnly();
        private readonly List<CaseParty> _parties = new();
        private readonly List<CaseNote> _notes = new();
        private readonly List<CaseStatusHistory> _statusHistory = new();


    }
}
