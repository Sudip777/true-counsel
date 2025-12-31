using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.LegalCase.Commands;
using TrueCounsel.Application.Features.LegalCase.Dtos;

namespace TrueCounsel.Application.Features.LegalCase.Commands.Handlers
{
    public class UpdateLegalCaseHandler : IRequestHandler<UpdateLegalCaseCommand, LegalCaseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLegalCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LegalCaseDto> Handle(UpdateLegalCaseCommand request, CancellationToken cancellationToken)
        {
            var legalCase = await _unitOfWork.LegalCaseRepository.GetByIdAsync(request.Id);
            if (legalCase == null) return null!;

            _mapper.Map(request, legalCase);
            legalCase.LastModifiedAt = DateTime.UtcNow;

            await _unitOfWork.LegalCaseRepository.UpdateAsync(legalCase);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<LegalCaseDto>(legalCase);
        }
    }
}
