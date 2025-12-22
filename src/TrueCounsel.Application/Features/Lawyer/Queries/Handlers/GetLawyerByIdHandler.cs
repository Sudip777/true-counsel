using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Lawyer.Dtos;

namespace TrueCounsel.Application.Features.Lawyer.Queries.Handlers
{
    public class GetLawyerByIdHandler : IQueryHandler<GetLawyerByIdQuery, LawyerDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLawyerByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LawyerDto?> HandleAsync(GetLawyerByIdQuery query, CancellationToken cancellationToken = default)
        {
            // check for existance
            var lawyer = await _unitOfWork.LawyerRepository.GetByIdAsync(query.Id);
            return lawyer == null ? null : _mapper.Map<LawyerDto>(lawyer);
        }
    }
}
