using MediatR;
using TrueCounsel.Application.Features.LegalCase.Dtos;

namespace TrueCounsel.Application.Features.LegalCase.Queries
{
    public class GetLegalCaseByIdQuery : IRequest<LegalCaseDto?>
    {
        public int Id { get; set; }

        public GetLegalCaseByIdQuery(int id)
        {
            Id = id;
        }
    }
}
