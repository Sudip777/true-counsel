using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Client.Dtos;
using TrueCounsel.Application.Features.Client.Queries;

namespace TrueCounsel.Application.Features.Client.Queries.Handlers
{
    public class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, ClientDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClientByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.ClientRepository.GetByIdAsync(request.Id);
            return client == null ? null : _mapper.Map<ClientDto>(client);
        }
    }
}
