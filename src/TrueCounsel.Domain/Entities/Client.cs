using System;
using System.Collections.Generic;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.Entities
{
    
    public class Client : BaseAuditableEntity
    {
        public int UserId { get;  set; }
        public User User { get;  set; } = null!;

        public string? Phone { get;  set; }
        public string? Address { get;  set; }                  
        public DateTime? DateOfBirth { get;  set; }
        public string? Occupation { get;  set; }
        public string? EmergencyContact { get;  set; }  

        // Navigation
        public IReadOnlyCollection<LegalCase> Cases => _cases.AsReadOnly();
        private readonly List<LegalCase> _cases = new();

       
    }
}
