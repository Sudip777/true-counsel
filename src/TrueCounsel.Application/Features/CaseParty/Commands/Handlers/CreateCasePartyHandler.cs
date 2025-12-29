using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.CaseParty.Commands;
using TrueCounsel.Application.Features.CaseParty.Dtos;

namespace TrueCounsel.Application.Features.CaseParty.Commands.Handlers
{
    public class CreateCasePartyHandler : IRequestHandler<CreateCasePartyCommand, CasePartyDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCasePartyHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CasePartyDto> Handle(CreateCasePartyCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var caseParty = _mapper.Map<TrueCounsel.Domain.Entities.CaseParty>(request);
            caseParty.CreatedAt = DateTime.UtcNow;
            
            await _unitOfWork.CasePartyRepository.AddAsync(caseParty);
            await _unitOfWork.CommitAsync();

            var createdCaseParty = await _unitOfWork.CasePartyRepository.GetByIdAsync(caseParty.Id);
            return _mapper.Map<CasePartyDto>(createdCaseParty ?? caseParty);
        }
    }
}
