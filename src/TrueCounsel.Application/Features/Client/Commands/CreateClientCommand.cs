using MediatR;
using System;
using TrueCounsel.Application.Features.Client.Dtos;

namespace TrueCounsel.Application.Features.Client.Commands
{
    public class CreateClientCommand : IRequest<ClientDto>
    {
        public int UserId { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public string? EmergencyContact { get; set; }
    }
}
