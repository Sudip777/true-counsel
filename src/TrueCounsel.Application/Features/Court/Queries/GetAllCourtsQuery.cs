using MediatR;
using System.Collections.Generic;
using TrueCounsel.Application.Features.Court.Dtos;

namespace TrueCounsel.Application.Features.Court.Queries
{
    /// <summary>
    /// Query to retrieve all active courts (excluding soft-deleted records).
    /// </summary>
    public class GetAllCourtsQuery : IRequest<IEnumerable<CourtDto>>
    {
    }
}
