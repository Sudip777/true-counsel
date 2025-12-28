using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.CaseCategory.Dtos;
using TrueCounsel.Application.Features.CaseCategory.Queries;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Application.Features.CaseCategory.Queries.Handlers
{
    public class GetAllCaseCategoryHandler : IRequestHandler<GetAllCaseCategoryQuery, IEnumerable<CaseCategoryDto>>
    {
        private readonly ICaseCategory _caseCategoryRepository;
        private readonly IMapper _mapper;

        public GetAllCaseCategoryHandler(ICaseCategory caseCategoryRepository, IMapper mapper)
        {
            _caseCategoryRepository = caseCategoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CaseCategoryDto>> Handle(GetAllCaseCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await _caseCategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CaseCategoryDto>>(categories);
        }
    }
}
