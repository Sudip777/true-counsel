using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Client.Dtos;
using TrueCounsel.Application.Features.Client.Queries;

namespace TrueCounsel.Application.Features.Client.Queries.Handlers
{
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllClientsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _unitOfWork.ClientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }
    }
}
