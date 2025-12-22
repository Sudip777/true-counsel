using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Lawyer.Dtos;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Application.Features.Lawyer.Queries.Handlers
{
    public class GetAllLawyersHandler : IQueryHandler<GetAllLawyersQuery, IEnumerable<LawyerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllLawyersHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LawyerDto>> HandleAsync(GetAllLawyersQuery query, CancellationToken cancellationToken = default)
        {
            // fetch all lawyrs from repo
            var lawyers = await _unitOfWork.LawyerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LawyerDto>>(lawyers);
        }
    }
}
