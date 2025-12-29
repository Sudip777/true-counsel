using MediatR;
using TrueCounsel.Application.Features.Court.Dtos;

namespace TrueCounsel.Application.Features.Court.Commands
{
    /// <summary>
    /// Command to update an existing court.
    /// </summary>
    public class UpdateCourtCommand : IRequest<CourtDto>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
