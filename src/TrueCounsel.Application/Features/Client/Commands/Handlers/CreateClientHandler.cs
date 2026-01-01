using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Exceptions;
using TrueCounsel.Application.Features.Client.Commands;
using TrueCounsel.Application.Features.Client.Dtos;

namespace TrueCounsel.Application.Features.Client.Commands.Handlers
{
    public class CreateClientHandler : IRequestHandler<CreateClientCommand, ClientDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateClientHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ClientDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            if (user == null) throw new Exception($"User with id {request.UserId} not found.");

            var client = _mapper.Map<TrueCounsel.Domain.Entities.Client>(request);
            client.CreatedAt = DateTime.UtcNow;
            
            await _unitOfWork.ClientRepository.AddAsync(client);
            await _unitOfWork.CommitAsync();

            // Fetch again to include User details if needed, or just map what we have
            var createdClient = await _unitOfWork.ClientRepository.GetByIdAsync(client.Id);
            return _mapper.Map<ClientDto>(createdClient ?? client);
        }
    }
}
