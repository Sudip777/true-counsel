using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.CaseCategory.Commands;
using TrueCounsel.Application.Features.CaseCategory.Dtos;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Application.Features.CaseCategory.Commands.Handlers
{
    public class CreateCaseCategoryHandler : IRequestHandler<CreateCaseCategoryCommand, CaseCategoryDto>
    {
        private readonly ICaseCategory _caseCategoryRepository;
        private readonly IMapper _mapper;

        public CreateCaseCategoryHandler(ICaseCategory caseCategoryRepository, IMapper mapper)
        {
            _caseCategoryRepository = caseCategoryRepository;
            _mapper = mapper;
        }

        public async Task<CaseCategoryDto> Handle(CreateCaseCategoryCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var caseCategory = _mapper.Map<TrueCounsel.Domain.Entities.CaseCategory>(request);
            caseCategory.CreatedAt = DateTime.UtcNow;

            await _caseCategoryRepository.AddAsync(caseCategory);

            return _mapper.Map<CaseCategoryDto>(caseCategory);
        }
    }
}
