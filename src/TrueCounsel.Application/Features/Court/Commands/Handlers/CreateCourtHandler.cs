using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Court.Commands;
using TrueCounsel.Application.Features.Court.Dtos;

namespace TrueCounsel.Application.Features.Court.Commands.Handlers
{
    /// <summary>
    /// Handler for CreateCourtCommand.
    /// Creates a new court record and commits it through the Unit of Work pattern.
    /// </summary>
    public class CreateCourtHandler : IRequestHandler<CreateCourtCommand, CourtDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCourtHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CourtDto> Handle(CreateCourtCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Map the command to the domain entity
            var court = new TrueCounsel.Domain.Entities.Court
            {
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                State = request.State,
                CreatedAt = DateTime.UtcNow
            };
            
            // Add through repository and commit via Unit of Work
            await _unitOfWork.CourtRepository.AddAsync(court);
            await _unitOfWork.CommitAsync();

            // Fetch the created court to return complete DTO
            var createdCourt = await _unitOfWork.CourtRepository.GetByIdAsync(court.Id);
            return _mapper.Map<CourtDto>(createdCourt ?? court);
        }
    }
}
