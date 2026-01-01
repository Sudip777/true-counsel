using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.LegalCase.Dtos;
using TrueCounsel.Application.Features.LegalCase.Queries;

namespace TrueCounsel.Application.Features.LegalCase.Queries.Handlers
{
    public class GetAllLegalCasesHandler : IRequestHandler<GetAllLegalCasesQuery, IEnumerable<LegalCaseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllLegalCasesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LegalCaseDto>> Handle(GetAllLegalCasesQuery request, CancellationToken cancellationToken)
        {
            var legalCases = await _unitOfWork.LegalCaseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LegalCaseDto>>(legalCases);
        }
    }
}
