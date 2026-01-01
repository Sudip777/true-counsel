using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.CaseParty.Dtos;
using TrueCounsel.Application.Features.CaseParty.Queries;

namespace TrueCounsel.Application.Features.CaseParty.Queries.Handlers
{
    public class GetAllCasePartiesHandler : IRequestHandler<GetAllCasePartiesQuery, IEnumerable<CasePartyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCasePartiesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CasePartyDto>> Handle(GetAllCasePartiesQuery request, CancellationToken cancellationToken)
        {
            var caseParties = await _unitOfWork.CasePartyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CasePartyDto>>(caseParties);
        }
    }
}
