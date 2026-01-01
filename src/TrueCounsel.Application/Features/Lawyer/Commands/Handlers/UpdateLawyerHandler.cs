using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TrueCounsel.Application.Common.Exceptions;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Dtos;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Application.Features.Lawyer.Commands.Handlers
{
    public class UpdateLawyerHandler : IRequestHandler<UpdateLawyerCommand, LawyerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FluentValidation.IValidator<UpdateLawyerCommand> _validator;

        public UpdateLawyerHandler(IUnitOfWork unitOfWork, IMapper mapper, FluentValidation.IValidator<UpdateLawyerCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary> updates exsting lawer deetails </summary>
        public async Task<LawyerDto> Handle(UpdateLawyerCommand command, CancellationToken cancellationToken)
        {
             ArgumentNullException.ThrowIfNull(command);

             var validationResult = await _validator.ValidateAsync(command, cancellationToken);
             if (!validationResult.IsValid)
             {
                 throw new AppValidationException(validationResult.Errors);
             }

             var lawyer = await _unitOfWork.LawyerRepository.GetByIdAsync(command.Id);
             if (lawyer == null) throw new KeyNotFoundException($"Lawyer with id {command.Id} not found");

             _mapper.Map(command, lawyer);
             lawyer.UpdatedAt = DateTime.UtcNow;

             await _unitOfWork.LawyerRepository.UpdateAsync(lawyer);
             await _unitOfWork.CommitAsync();

             return _mapper.Map<LawyerDto>(lawyer);
        }
    }
}
