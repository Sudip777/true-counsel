using MediatR;
using System.Collections.Generic;
using TrueCounsel.Application.Features.LegalCase.Dtos;

namespace TrueCounsel.Application.Features.LegalCase.Queries
{
    public class GetAllLegalCasesQuery : IRequest<IEnumerable<LegalCaseDto>>
    {
    }
}
