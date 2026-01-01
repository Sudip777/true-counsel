using MediatR;
using TrueCounsel.Application.Features.CaseNote.Dtos;
using TrueCounsel.Domain.Enums;

namespace TrueCounsel.Application.Features.CaseNote.Commands
{
    public class CreateCaseNoteCommand : IRequest<CaseNoteDto>
    {
        public int CaseId { get; set; }
        public int AuthorId { get; set; }
        public CaseNoteType CaseNoteType { get; set; }
        public required string Note { get; set; }
        public bool IsPrivate { get; set; }
    }
}
