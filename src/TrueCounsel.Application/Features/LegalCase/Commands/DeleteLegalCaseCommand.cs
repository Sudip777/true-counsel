using MediatR;

namespace TrueCounsel.Application.Features.LegalCase.Commands
{
    public class DeleteLegalCaseCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteLegalCaseCommand(int id)
        {
            Id = id;
        }
    }
}
