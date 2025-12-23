using MediatR;
using System.Collections.Generic;
using TrueCounsel.Application.Features.Client.Dtos;

namespace TrueCounsel.Application.Features.Client.Queries
{
    public class GetAllClientsQuery : IRequest<IEnumerable<ClientDto>>
    {
    }
}
