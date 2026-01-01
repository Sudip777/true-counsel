using MediatR;

namespace TrueCounsel.Application.Features.Client.Commands
{
    public class DeleteClientCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteClientCommand(int id)
        {
            Id = id;
        }
    }
}
