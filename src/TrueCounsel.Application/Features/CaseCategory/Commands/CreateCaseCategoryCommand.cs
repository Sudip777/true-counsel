using MediatR;
using TrueCounsel.Application.Features.CaseCategory.Dtos;

namespace TrueCounsel.Application.Features.CaseCategory.Commands
{
    public class CreateCaseCategoryCommand : IRequest<CaseCategoryDto>
    {
        public int CaseTypeId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
