using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TrueCounsel.Application.Features.Lawyer.Dtos;
using TrueCounsel.Domain.Interfaces;
using TrueCounsel.Application.Common.Abstractions;


namespace TrueCounsel.Application.Features.Lawyer.Queries.Handlers
{
    public class GetLawyerByIdHandler : IRequestHandler<GetLawyerByIdQuery, LawyerDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLawyerByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LawyerDto?> Handle(GetLawyerByIdQuery query, CancellationToken cancellationToken)
        {
            // check for existance
            var lawyer = await _unitOfWork.LawyerRepository.GetByIdAsync(query.Id);
            return lawyer == null ? null : _mapper.Map<LawyerDto>(lawyer);
        }
    }
}
