using MediatR;
using System;
using TrueCounsel.Application.Features.LegalCase.Dtos;
using TrueCounsel.Domain.Enums;

namespace TrueCounsel.Application.Features.LegalCase.Commands
{
    public class CreateLegalCaseCommand : IRequest<LegalCaseDto>
    {
        public string CaseNumber { get; set; } = string.Empty;
        public string? CourtCaseNumber { get; set; }
        public int ClientId { get; set; }
        public int LawyerId { get; set; }
        public int? CaseCategoryId { get; set; }
        public int? CourtId { get; set; }
        public int CaseTypeId { get; set; }
        public string Title { get; set; } = string.Empty;
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
