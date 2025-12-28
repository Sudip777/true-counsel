using MediatR;
using System.Collections.Generic;
using TrueCounsel.Application.Features.CaseType.Dtos;

namespace TrueCounsel.Application.Features.CaseType.Queries
{
    public class GetAllCaseTypeQuery : IRequest<IEnumerable<CaseTypeDto>>
    {
    }
}
