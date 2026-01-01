using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.CaseType.Commands;
using TrueCounsel.Application.Features.CaseType.Dtos;
using TrueCounsel.Domain.Interfaces;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Application.Features.CaseType.Commands.Handlers
{
    public class CreateCaseTypeHandler : IRequestHandler<CreateCaseTypeCommand, CaseTypeDto>
    {
        private readonly ICaseTypeRepository _caseTypeRepository;
        private readonly IMapper _mapper;

        public CreateCaseTypeHandler(ICaseTypeRepository caseTypeRepository, IMapper mapper)
        {
            _caseTypeRepository = caseTypeRepository;
            _mapper = mapper;
        }

        public async Task<CaseTypeDto> Handle(CreateCaseTypeCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var caseType = _mapper.Map<TrueCounsel.Domain.Entities.CaseType>(request);
            caseType.CreatedAt = DateTime.UtcNow;

            await _caseTypeRepository.AddAsync(caseType);

            return _mapper.Map<CaseTypeDto>(caseType);
        }
    }
}
