using MediatR;
using TrueCounsel.Application.Features.Client.Dtos;

namespace TrueCounsel.Application.Features.Client.Queries
{
    public class GetClientByIdQuery : IRequest<ClientDto?>
    {
        public int Id { get; set; }

        public GetClientByIdQuery(int id)
        {
            Id = id;
        }
    }
}
