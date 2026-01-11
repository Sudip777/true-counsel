using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TrueCounsel.Application.Features.Lawyer.Dtos;
using TrueCounsel.Domain.Interfaces;
using TrueCounsel.Application.Common.Abstractions;


namespace TrueCounsel.Application.Features.Lawyer.Queries.Handlers
{
    public class GetAllLawyersHandler : IRequestHandler<GetAllLawyersQuery, IEnumerable<LawyerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllLawyersHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LawyerDto>> Handle(GetAllLawyersQuery query, CancellationToken cancellationToken)
        {
            // fetch all lawyrs from repo
            var lawyers = await _unitOfWork.LawyerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LawyerDto>>(lawyers);
        }
    }
}
