using TrueCounsel.Domain.Common;
using TrueCounsel.Domain.Enums;

namespace TrueCounsel.Domain.Entities
{
   
    public class User : BaseAuditableEntity
    {
        public required  string Name { get;  set; } 
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public Uri? ProfilePhotoUrl { get;  set; }
        public UserRole Role { get;  set; } = UserRole.Client;
        public bool IsActive { get;  set; } = true;
    }
}
