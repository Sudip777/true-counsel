using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.LegalCase.Commands;
using TrueCounsel.Application.Features.LegalCase.Dtos;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Application.Features.LegalCase.Commands.Handlers
{
    public class CreateLegalCaseHandler : IRequestHandler<CreateLegalCaseCommand, LegalCaseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLegalCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LegalCaseDto> Handle(CreateLegalCaseCommand request, CancellationToken cancellationToken)
        {
            var legalCase = _mapper.Map<TrueCounsel.Domain.Entities.LegalCase>(request);
            legalCase.CreatedAt = DateTime.UtcNow;
            
            await _unitOfWork.LegalCaseRepository.AddAsync(legalCase);
            await _unitOfWork.CommitAsync();

            var createdCase = await _unitOfWork.LegalCaseRepository.GetByIdAsync(legalCase.Id);
            return _mapper.Map<LegalCaseDto>(createdCase ?? legalCase);
        }
    }
}
