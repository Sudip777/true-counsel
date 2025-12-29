using MediatR;
using TrueCounsel.Application.Features.Court.Dtos;

namespace TrueCounsel.Application.Features.Court.Commands
{
    /// <summary>
    /// Command to create a new court.
    /// </summary>
    public class CreateCourtCommand : IRequest<CourtDto>
    {
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int CourtTypeId { get; set; }
    }
}
