using MediatR;
using System.Collections.Generic;
using TrueCounsel.Application.Features.Lawyer.Dtos;

namespace TrueCounsel.Application.Features.Lawyer.Queries
{
    public class GetAllLawyersQuery : IRequest<IEnumerable<LawyerDto>>
    {
    }
}
