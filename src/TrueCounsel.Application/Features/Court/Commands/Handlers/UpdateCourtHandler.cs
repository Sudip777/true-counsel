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
    /// Handler for UpdateCourtCommand.
    /// Updates an existing court record and commits it through the Unit of Work pattern.
    /// </summary>
    public class UpdateCourtHandler : IRequestHandler<UpdateCourtCommand, CourtDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCourtHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CourtDto> Handle(UpdateCourtCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Retrieve the existing court
            var court = await _unitOfWork.CourtRepository.GetByIdAsync(request.Id);
            if (court == null)
                throw new Exception($"Court with id {request.Id} not found.");

            // Update the entity properties
            court.Name = request.Name;
            court.Address = request.Address;
            court.City = request.City;
            court.State = request.State;
            court.UpdatedAt = DateTime.UtcNow;

            // Update through repository and commit via Unit of Work
            await _unitOfWork.CourtRepository.UpdateAsync(court);
            await _unitOfWork.CommitAsync();

            // Fetch the updated court to return complete DTO
            var updatedCourt = await _unitOfWork.CourtRepository.GetByIdAsync(court.Id);
            return _mapper.Map<CourtDto>(updatedCourt ?? court);
        }
    }
}
