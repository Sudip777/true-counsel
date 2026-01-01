using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.CaseNote.Commands;
using TrueCounsel.Application.Features.CaseNote.Dtos;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Application.Features.CaseNote.Commands.Handlers
{
    public class CreateCaseNoteHandler : IRequestHandler<CreateCaseNoteCommand, CaseNoteDto>
    {
        private readonly ICaseNoteRepository _caseNoteRepository;
        private readonly IMapper _mapper;

        public CreateCaseNoteHandler(ICaseNoteRepository caseNoteRepository, IMapper mapper)
        {
            _caseNoteRepository = caseNoteRepository;
            _mapper = mapper;
        }

        public async Task<CaseNoteDto> Handle(CreateCaseNoteCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var caseNote = _mapper.Map<TrueCounsel.Domain.Entities.CaseNote>(request);
            caseNote.CreatedAt = DateTime.UtcNow;

            await _caseNoteRepository.AddAsync(caseNote);

            return _mapper.Map<CaseNoteDto>(caseNote);
        }
    }
}
