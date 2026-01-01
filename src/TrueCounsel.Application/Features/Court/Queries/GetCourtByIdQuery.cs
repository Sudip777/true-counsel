using MediatR;
using TrueCounsel.Application.Features.Court.Dtos;

namespace TrueCounsel.Application.Features.Court.Queries
{
    /// <summary>
    /// Query to retrieve a court by ID.
    /// Returns null if the court is not found or has been soft-deleted.
    /// </summary>
    public class GetCourtByIdQuery : IRequest<CourtDto?>
    {
        public int Id { get; set; }

        public GetCourtByIdQuery(int id)
        {
            Id = id;
        }
    }
}
