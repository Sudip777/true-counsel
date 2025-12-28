namespace TrueCounsel.Application.Features.CaseType.Commands
{
    public class CreateCaseTypeCommand : IRequest<CaseTypeDto>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
