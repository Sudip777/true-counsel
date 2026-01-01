using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.CaseType.Dtos;
using TrueCounsel.Application.Features.CaseType.Queries;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Application.Features.CaseType.Queries.Handlers
{
    public class GetAllCaseTypeHandler : IRequestHandler<GetAllCaseTypeQuery, IEnumerable<CaseTypeDto>>
    {
        private readonly ICaseTypeRepository _caseTypeRepository;
        private readonly IMapper _mapper;

        public GetAllCaseTypeHandler(ICaseTypeRepository caseTypeRepository, IMapper mapper)
        {
            _caseTypeRepository = caseTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CaseTypeDto>> Handle(GetAllCaseTypeQuery request, CancellationToken cancellationToken)
        {
            var caseTypes = await _caseTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CaseTypeDto>>(caseTypes);
        }
    }
}
