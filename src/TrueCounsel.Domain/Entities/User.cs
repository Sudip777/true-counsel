using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;
using TrueCounsel.Domain.Enums;
using TrueCounsel.Domain.ValueObjects;

namespace TrueCounsel.Domain.Entities
{
   
    public class User : BaseAuditableEntity
    {
        public string Name { get; private set; } = String.Empty;
        public Email Email { get; private set; } = null!; // ← VO
        public string PasswordHash { get; private set; } = String.Empty;
        public Uri? ProfilePhotoUrl { get; private set; }
        public UserRole Role { get; private set; } = UserRole.Client;
        public bool IsActive { get; private set; } = true;

        public static User Create(string name, string email, string passwordHash, UserRole role = UserRole.Client)
        {
            return new User
            {
                Name = name,
                Email = Email.Create(email),
                PasswordHash = passwordHash,
                Role = role,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
