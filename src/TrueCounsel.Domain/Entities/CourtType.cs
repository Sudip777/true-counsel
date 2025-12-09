using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;
using TrueCounsel.Domain.Enums;

namespace TrueCounsel.Domain.Entities
{
    public class Court : BaseAuditableEntity
    {
        public string Name { get; private set; } = String.Empty;
        public string? Address { get; private set; }
        public string? City { get; private set; }
        public string? State { get; private set; }
        public CourtType CourtType { get; private set; }

        public IReadOnlyCollection<Case> Cases => _cases.AsReadOnly();
        private readonly List<Case> _cases = new();

        private Court() { }

        public static Court Create(string name, CourtType courtType, string? address = null, string? city = null, string? state = null)
        {
            ArgumentNullException.ThrowIfNull(name);

            return new Court
            {
                Name = name.Trim(),
                Address = address?.Trim(),
                City = city?.Trim(),
                State = state?.Trim(),
                CourtType = courtType,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
