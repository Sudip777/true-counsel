using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Domain.Entities
{
    public class CaseCategory : BaseAuditableEntity
    {
        public int CaseTypeId { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }

        public CaseType CaseType { get; private set; } = null!;
        public IReadOnlyCollection<Case> Cases => _cases.AsReadOnly();

        private readonly List<Case> _cases = new();

        private CaseCategory() { }

        public static CaseCategory Create(CaseType caseType, string name, string? description = null)
        {
            ArgumentNullException.ThrowIfNull(@caseType);
            ArgumentNullException.ThrowIfNull(@name);


            return new CaseCategory
            {
                CaseTypeId = caseType.Id,
                CaseType = caseType,
                Name = name.Trim(),
                Description = description?.Trim(),
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
