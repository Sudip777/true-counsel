using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Dtos;

namespace TrueCounsel.Application.Features.Lawyer.Commands.Handlers
{
    public class UpdateLawyerHandler : ICommandHandler<UpdateLawyerCommand, LawyerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLawyerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary> updates exsting lawer deetails </summary>
        public async Task<LawyerDto> HandleAsync(UpdateLawyerCommand command, CancellationToken cancellationToken = default)
        {
             ArgumentNullException.ThrowIfNull(command);

             var lawyer = await _unitOfWork.LawyerRepository.GetByIdAsync(command.Id);
             if (lawyer == null) throw new Exception($"Lawyer with id {command.Id} not found");

             _mapper.Map(command, lawyer);
             lawyer.UpdatedAt = DateTime.UtcNow;

             await _unitOfWork.LawyerRepository.UpdateAsync(lawyer);
             await _unitOfWork.CommitAsync();

             return _mapper.Map<LawyerDto>(lawyer);
        }
    }
}
