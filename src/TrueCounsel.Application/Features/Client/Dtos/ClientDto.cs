using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Application.Features.Client.Dtos
{
    public class ClientDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public string? EmergencyContact { get; set; }

    }
}
