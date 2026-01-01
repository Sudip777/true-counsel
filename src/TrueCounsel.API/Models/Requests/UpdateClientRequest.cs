using System;

namespace TrueCounsel.API.Models.Requests
{
    public class UpdateClientRequest
    {
        public int UserId { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public string? EmergencyContact { get; set; }
    }
}
