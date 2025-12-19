using System;
using TrueCounsel.Domain.Enums;

namespace TrueCounsel.Application.Common.Models
{
    public class UserDto
    {
        public int Id { get; set; }           
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Uri? ProfilePhotoUrl { get; set; }
        public UserRole Role { get; set; } = UserRole.Client;
        public bool IsActive { get; set; } = true;
    }
   


}
