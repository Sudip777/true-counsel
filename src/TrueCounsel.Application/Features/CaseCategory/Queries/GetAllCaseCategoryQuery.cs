using MediatR;
using System.Collections.Generic;
using TrueCounsel.Application.Features.CaseCategory.Dtos;

namespace TrueCounsel.Application.Features.CaseCategory.Queries
{
    public class GetAllCaseCategoryQuery : IRequest<IEnumerable<CaseCategoryDto>>
    {
    }
}
