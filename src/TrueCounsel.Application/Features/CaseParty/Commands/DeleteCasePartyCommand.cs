using MediatR;

namespace TrueCounsel.Application.Features.CaseParty.Commands
{
    public class DeleteCasePartyCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteCasePartyCommand(int id)
        {
            Id = id;
        }
    }
}
