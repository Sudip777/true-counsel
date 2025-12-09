using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;
using TrueCounsel.Domain.ValueObjects;

namespace TrueCounsel.Domain.Entities
{
    
    public class Client : BaseAuditableEntity
    {
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;

        public string? Phone { get; private set; }
        public Address? Address { get; private set; }                    // ← VO
        public DateTime? DateOfBirth { get; private set; }
        public string? Occupation { get; private set; }
        public EmergencyContact? EmergencyContact { get; private set; }  // ← VO

        // Navigation
        public IReadOnlyCollection<Case> Cases => _cases.AsReadOnly();
        private readonly List<Case> _cases = new();

        private Client() { }

        public static Client Create(User user,
            string? phone = null,
            Address? address = null,
            DateTime? dateOfBirth = null,
            string? occupation = null,
            EmergencyContact? emergencyContact = null)
        {
            ArgumentNullException.ThrowIfNull(user);

            return new Client
            {
                UserId = user.Id,
                User = user,
                Phone = phone,
                Address = address,
                DateOfBirth = dateOfBirth,
                Occupation = occupation,
                EmergencyContact = emergencyContact,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void UpdateContactInfo(
            string? phone = null,
            Address? address = null,
            EmergencyContact? emergencyContact = null)
        {
            Phone = phone ?? Phone;
            Address = address ?? Address;
            EmergencyContact = emergencyContact ?? EmergencyContact;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
