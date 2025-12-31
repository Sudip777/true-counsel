using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.LegalCase.Dtos;
using TrueCounsel.Application.Features.LegalCase.Queries;

namespace TrueCounsel.Application.Features.LegalCase.Queries.Handlers
{
    public class GetLegalCaseByIdHandler : IRequestHandler<GetLegalCaseByIdQuery, LegalCaseDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLegalCaseByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LegalCaseDto?> Handle(GetLegalCaseByIdQuery request, CancellationToken cancellationToken)
        {
            var legalCase = await _unitOfWork.LegalCaseRepository.GetByIdAsync(request.Id);
            return _mapper.Map<LegalCaseDto?>(legalCase);
        }
    }
}
