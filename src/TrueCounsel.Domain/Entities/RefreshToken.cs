using System;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.Entities
{
    public class RefreshToken : BaseAuditableEntity
    {
        public string Token { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime? Revoked { get; set; }
        public bool IsRevoked => Revoked != null;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}