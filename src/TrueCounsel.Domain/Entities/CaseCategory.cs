using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.Entities
{
    public class CaseCategory : BaseAuditableEntity
    {
        public int CaseTypeId { get;  set; }
        public string Name { get;  set; } = null!;
        public string? Description { get;  set; }

        public CaseType CaseType { get;  set; } = null!;
        public IReadOnlyCollection<LegalCase> Cases => _cases.AsReadOnly();

        private readonly List<LegalCase> _cases = new();

    }
}
