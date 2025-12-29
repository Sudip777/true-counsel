using MediatR;
using TrueCounsel.Application.Features.CaseParty.Dtos;

namespace TrueCounsel.Application.Features.CaseParty.Queries
{
    public class GetCasePartyByIdQuery : IRequest<CasePartyDto?>
    {
        public int Id { get; set; }

        public GetCasePartyByIdQuery(int id)
        {
            Id = id;
        }
    }
}
