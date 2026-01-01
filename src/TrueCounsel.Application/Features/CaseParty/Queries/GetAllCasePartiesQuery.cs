using MediatR;
using System.Collections.Generic;
using TrueCounsel.Application.Features.CaseParty.Dtos;

namespace TrueCounsel.Application.Features.CaseParty.Queries
{
    public class GetAllCasePartiesQuery : IRequest<IEnumerable<CasePartyDto>>
    {
    }
}
