using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Court.Dtos;
using TrueCounsel.Application.Features.Court.Queries;

namespace TrueCounsel.Application.Features.Court.Queries.Handlers
{
    /// <summary>
    /// Handler for GetCourtByIdQuery.
    /// Retrieves a specific court by ID. Returns null if not found or if the court has been soft-deleted.
    /// </summary>
    public class GetCourtByIdHandler : IRequestHandler<GetCourtByIdQuery, CourtDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCourtByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CourtDto?> Handle(GetCourtByIdQuery request, CancellationToken cancellationToken)
        {
            var court = await _unitOfWork.CourtRepository.GetByIdAsync(request.Id);
            return court == null ? null : _mapper.Map<CourtDto>(court);
        }
    }
}
