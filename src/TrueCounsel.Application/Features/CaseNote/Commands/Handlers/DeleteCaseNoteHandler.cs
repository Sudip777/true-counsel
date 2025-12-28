using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.CaseNote.Commands;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Application.Features.CaseNote.Commands.Handlers
{
    public class DeleteCaseNoteHandler : IRequestHandler<DeleteCaseNoteCommand, bool>
    {
        private readonly ICaseNoteRepository _caseNoteRepository;

        public DeleteCaseNoteHandler(ICaseNoteRepository caseNoteRepository)
        {
            _caseNoteRepository = caseNoteRepository;
        }

        public async Task<bool> Handle(DeleteCaseNoteCommand request, CancellationToken cancellationToken)
        {
            return await _caseNoteRepository.DeleteAsync(request.Id);
        }
    }
}
