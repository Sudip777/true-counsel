using System;
using TrueCounsel.Domain.Enums;

namespace TrueCounsel.API.Models.Requests
{
    public class CreateLegalCaseRequest
    {
        public required string CaseNumber { get; set; }
        public string? CourtCaseNumber { get; set; }
        public int ClientId { get; set; }
        public int LawyerId { get; set; }
        public int? CaseCategoryId { get; set; }
        public int? CourtId { get; set; }
        public int CaseTypeId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public CaseStatus Status { get; set; } = CaseStatus.New;
        public CasePriority Priority { get; set; } = CasePriority.Medium;
        public DateTime? FilingDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public CaseOutcome? Outcome { get; set; }
        public string? OutcomeNotes { get; set; }
        public string Tags { get; set; } = string.Empty;
        public decimal EstimatedValue { get; set; }
    }
}
