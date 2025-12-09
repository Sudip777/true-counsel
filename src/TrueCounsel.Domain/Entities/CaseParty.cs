using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;
using TrueCounsel.Domain.Enums;

namespace TrueCounsel.Domain.Entities
{
    public class CaseParty : BaseAuditableEntity
    {
        public int CaseId { get; private set; }
        public string Name { get; private set; } = null!;
        public CasePartyRole Role { get; private set; }
        public string? Organization { get; private set; }
        public string? ContactEmail { get; private set; }
        public string? ContactPhone { get; private set; }
        public string? Address { get; private set; }
        public string? Notes { get; private set; }

        public Case Case { get; private set; } = null!;

        private CaseParty() { }

        public static CaseParty Create(Case @case, string name, CasePartyRole role, string? organization = null, string? email = null, string? phone = null)
        {
            ArgumentNullException.ThrowIfNull(@case);
            ArgumentNullException.ThrowIfNull(name);

            return new CaseParty
            {
                CaseId = @case.Id,
                Case = @case,
                Name = name.Trim(),
                Role = role,
                Organization = organization?.Trim(),
                ContactEmail = email,
                ContactPhone = phone,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
