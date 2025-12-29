using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Court.Dtos;
using TrueCounsel.Application.Features.Court.Queries;

namespace TrueCounsel.Application.Features.Court.Queries.Handlers
{
    /// <summary>
    /// Handler for GetAllCourtsQuery.
    /// Retrieves all active courts from the repository, automatically excluding soft-deleted records.
    /// </summary>
    public class GetAllCourtsHandler : IRequestHandler<GetAllCourtsQuery, IEnumerable<CourtDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCourtsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourtDto>> Handle(GetAllCourtsQuery request, CancellationToken cancellationToken)
        {
            var courts = await _unitOfWork.CourtRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourtDto>>(courts);
        }
    }
}
