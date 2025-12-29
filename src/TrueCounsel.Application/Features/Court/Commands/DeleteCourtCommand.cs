using MediatR;

namespace TrueCounsel.Application.Features.Court.Commands
{
    /// <summary>
    /// Command to delete (soft delete) a court.
    /// The DeletedAt timestamp will be set, preserving the record for audit purposes.
    /// </summary>
    public class DeleteCourtCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteCourtCommand(int id)
        {
            Id = id;
        }
    }
}
