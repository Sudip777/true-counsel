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
    public class UpdateClientHandler : IRequestHandler<UpdateClientCommand, ClientDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateClientHandler(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ClientDto> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var client = await _unitOfWork.ClientRepository.GetByIdAsync(request.Id);
            if (client == null) throw new Exception($"Client with id {request.Id} not found");

            _mapper.Map(request, client);
            client.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.ClientRepository.UpdateAsync(client);
            await _unitOfWork.CommitAsync();

            var updatedClient = await _unitOfWork.ClientRepository.GetByIdAsync(client.Id);
            return _mapper.Map<ClientDto>(updatedClient ?? client);
        }
    }
}
