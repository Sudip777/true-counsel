using MediatR;

namespace TrueCounsel.Application.Features.CaseNote.Commands
{
    public class DeleteCaseNoteCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteCaseNoteCommand(int id)
        {
            Id = id;
        }
    }
}
