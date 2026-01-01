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
    public class UpdateCasePartyHandler : IRequestHandler<UpdateCasePartyCommand, CasePartyDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCasePartyHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CasePartyDto> Handle(UpdateCasePartyCommand request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var caseParty = await _unitOfWork.CasePartyRepository.GetByIdAsync(request.Id);
            if (caseParty == null) throw new Exception($"CaseParty with id {request.Id} not found");

            _mapper.Map(request, caseParty);
            caseParty.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CasePartyRepository.UpdateAsync(caseParty);
            await _unitOfWork.CommitAsync();

            var updatedCaseParty = await _unitOfWork.CasePartyRepository.GetByIdAsync(caseParty.Id);
            return _mapper.Map<CasePartyDto>(updatedCaseParty ?? caseParty);
        }
    }
}
