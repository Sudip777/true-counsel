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
        public string? Description { get;  set; }

        // Navigation
        public IReadOnlyCollection<CaseCategory> Categories => _categories.AsReadOnly();
        public IReadOnlyCollection<LegalCase> LegalCases => _cases.AsReadOnly();

        private readonly List<CaseCategory> _categories = new();
        private readonly List<LegalCase> _cases = new();


       
    }
}

