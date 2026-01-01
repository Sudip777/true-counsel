using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.CaseNote.Dtos;
using TrueCounsel.Application.Features.CaseNote.Queries;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Application.Features.CaseNote.Queries.Handlers
{
    public class GetAllCaseNoteHandler : IRequestHandler<GetAllCaseNoteQuery, IEnumerable<CaseNoteDto>>
    {
        private readonly ICaseNoteRepository _caseNoteRepository;
        private readonly IMapper _mapper;

        public GetAllCaseNoteHandler(ICaseNoteRepository caseNoteRepository, IMapper mapper)
        {
            _caseNoteRepository = caseNoteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CaseNoteDto>> Handle(GetAllCaseNoteQuery request, CancellationToken cancellationToken)
        {
            var notes = await _caseNoteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CaseNoteDto>>(notes);
        }
    }
}
