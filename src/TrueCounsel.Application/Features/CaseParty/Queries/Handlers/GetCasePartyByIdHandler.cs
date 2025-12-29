using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.CaseParty.Dtos;
using TrueCounsel.Application.Features.CaseParty.Queries;

namespace TrueCounsel.Application.Features.CaseParty.Queries.Handlers
{
    public class GetCasePartyByIdHandler : IRequestHandler<GetCasePartyByIdQuery, CasePartyDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCasePartyByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CasePartyDto?> Handle(GetCasePartyByIdQuery request, CancellationToken cancellationToken)
        {
            var caseParty = await _unitOfWork.CasePartyRepository.GetByIdAsync(request.Id);
            return _mapper.Map<CasePartyDto?>(caseParty);
        }
    }
}
