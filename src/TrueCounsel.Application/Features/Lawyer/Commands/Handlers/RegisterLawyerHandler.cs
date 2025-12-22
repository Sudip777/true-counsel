using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Dtos;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Application.Features.Lawyer.Commands.Handlers
{
    public class RegisterLawyerHandler : ICommandHandler<RegisterLawyerCommand, LawyerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterLawyerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary> registrs a new lawer </summary>
        public async Task<LawyerDto> HandleAsync(RegisterLawyerCommand command, CancellationToken cancellationToken = default)
        {
            if(command == null) throw new ArgumentNullException(nameof(command));

            var user = await _unitOfWork.UserRepository.GetByIdAsync(command.UserId);
            if (user == null) throw new Exception($"User with id {command.UserId} not found.");

            var lawyer = _mapper.Map<TrueCounsel.Domain.Entities.Lawyer>(command);
            lawyer.CreatedAt = DateTime.UtcNow;
            // lawyer.User = user; // handled by FK UserId

            await _unitOfWork.LawyerRepository.AddAsync(lawyer);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<LawyerDto>(lawyer);
        }
    }
}
