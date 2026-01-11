using MediatR;

namespace TrueCounsel.Application.Features.Lawyer.Commands
{
    public class DeleteLawyerCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteLawyerCommand(int id)
        {
            Id = id;
        }
    }
}
