using System;
using System.Collections.Generic;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.Entities
{
    public class CourtType : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Navigation property for courts of this type
        public IReadOnlyCollection<Court> Courts => _courts.AsReadOnly();
        private readonly List<Court> _courts = new();
    }
}
