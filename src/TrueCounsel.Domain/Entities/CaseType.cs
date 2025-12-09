using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.Entities
{
    public class CaseType : BaseAuditableEntity
    {
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }

        // Navigation
        public IReadOnlyCollection<CaseCategory> Categories => _categories.AsReadOnly();
        public IReadOnlyCollection<Case> Cases => _cases.AsReadOnly();

        private readonly List<CaseCategory> _categories = new();
        private readonly List<Case> _cases = new();

        private CaseType() { } // EF Core

        public static CaseType Create(string name, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Case type name is required.", nameof(name));

            return new CaseType
            {
                Name = name.Trim(),
                Description = description?.Trim(),
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(string name, string? description = null)
        {
            ArgumentNullException.ThrowIfNull(name);

            Name = name.Trim();
            Description = description?.Trim();
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
