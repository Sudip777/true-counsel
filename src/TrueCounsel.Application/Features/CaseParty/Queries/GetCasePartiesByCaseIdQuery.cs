using MediatR;
using System.Collections.Generic;
using TrueCounsel.Application.Features.CaseParty.Dtos;

namespace TrueCounsel.Application.Features.CaseParty.Queries
{
    public class GetCasePartiesByCaseIdQuery : IRequest<IEnumerable<CasePartyDto>>
    {
        public int CaseId { get; set; }

        public GetCasePartiesByCaseIdQuery(int caseId)
        {
            CaseId = caseId;
        }
    }
}
