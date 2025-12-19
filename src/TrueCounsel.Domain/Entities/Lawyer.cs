using System.Collections.Generic;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.Entities
{
    public class Lawyer : BaseAuditableEntity
    {
        public int UserId { get;  set; }
        public User User { get;  set; } = null!;
        public string BarNumber { get;  set; } = null!;
        public string? Specialization { get;  set; }
        public int? YearsOfExperience { get;  set; }
        public required decimal HourlyRate { get; set; }
        public string? Bio { get;  set; }

        public IReadOnlyCollection<LegalCase> AssignedCases => _assignedCases.AsReadOnly();
        private readonly List<LegalCase> _assignedCases = new();

       
    }
}
