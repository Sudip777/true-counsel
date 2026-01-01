using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.CaseNote.Dtos;
using TrueCounsel.Application.Features.CaseNote.Queries;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Application.Features.CaseNote.Queries.Handlers
{
    public class GetCaseNoteByIdHandler : IRequestHandler<GetCaseNoteByIdQuery, CaseNoteDto?>
    {
        private readonly ICaseNoteRepository _caseNoteRepository;
        private readonly IMapper _mapper;

        public GetCaseNoteByIdHandler(ICaseNoteRepository caseNoteRepository, IMapper mapper)
        {
            _caseNoteRepository = caseNoteRepository;
            _mapper = mapper;
        }

        public async Task<CaseNoteDto?> Handle(GetCaseNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var note = await _caseNoteRepository.GetByIdAsync(request.Id);
            return note == null ? null : _mapper.Map<CaseNoteDto>(note);
        }
    }
}
