using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;
using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Enums;
using TrueCounsel.Domain.ValueObjects;

namespace TrueCounsel.Domain.Entities
{
    public class Case : BaseAuditableEntity
    {
        public CaseNumber CaseNumber { get; private set; } = null!;           // ← VO
        public string? CourtCaseNumber { get; private set; }

        public int ClientId { get; private set; }
        public int LawyerId { get; private set; }
        public int CaseTypeId { get; private set; }
        public int? CaseCategoryId { get; private set; }
        public int? CourtId { get; private set; }

        public string Title { get; private set; } = null!;
        public string? Description { get; private set; }
        public CaseStatus Status { get; private set; } = CaseStatus.New;
        public CasePriority Priority { get; private set; } = CasePriority.Medium;
        public DateTime? FilingDate { get; private set; }
        public DateTime? ClosingDate { get; private set; }
        public CaseOutcome? Outcome { get; private set; }
        public string? OutcomeNotes { get; private set; }
        public Money? EstimatedValue { get; private set; } // ← VO

        // Navigation properties
        public Client Client { get; private set; } = null!;
        public Lawyer Lawyer { get; private set; } = null!;
        public CaseType CaseType { get; private set; } = null!;
        public CaseCategory? CaseCategory { get; private set; }
        public Court? Court { get; private set; }

        public IReadOnlyCollection<CaseParty> Parties => _parties.AsReadOnly();
        public IReadOnlyCollection<CaseNote> Notes => _notes.AsReadOnly();
        public IReadOnlyCollection<CaseStatusHistory> StatusHistory => _statusHistory.AsReadOnly();
        public TagList Tags { get; private set; } = TagList.FromCsv("");      // ← VO

        private readonly List<CaseParty> _parties = new();
        private readonly List<CaseNote> _notes = new();
        private readonly List<CaseStatusHistory> _statusHistory = new();

        private Case() { }

        public static Case Create(
            Client client,
            Lawyer lawyer,
            CaseType caseType,
            string title,
            Money? estimatedValue = null,
            string? description = null,
            CasePriority priority = CasePriority.Medium,
            TagList? tags = null)
        {
            ArgumentNullException.ThrowIfNull(client);
            ArgumentNullException.ThrowIfNull(lawyer);
            ArgumentNullException.ThrowIfNull(caseType);
            var legalCase = new Case
            {
                CaseNumber = CaseNumber.Create(),
                ClientId = client.Id,
                Client = client,
                LawyerId = lawyer.Id,
                Lawyer = lawyer,
                CaseTypeId = caseType.Id,
                CaseType = caseType,
                Title = title,
                Description = description,
                Priority = priority,
                EstimatedValue = estimatedValue,
                Tags = tags ?? TagList.FromCsv(""),
                CreatedAt = DateTime.UtcNow
            };

            return legalCase;
        }

        public void UpdateEstimatedValue(Money newValue)
        {
            EstimatedValue = newValue;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddTags(TagList additionalTags)
        {
            var combined = Tags.Tags.Concat(additionalTags.Tags).Distinct();
            Tags = new TagList(combined);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
