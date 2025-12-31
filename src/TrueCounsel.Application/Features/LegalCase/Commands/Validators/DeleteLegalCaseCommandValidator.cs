using FluentValidation;
using TrueCounsel.Application.Features.LegalCase.Commands;

namespace TrueCounsel.Application.Features.LegalCase.Commands.Validators
{
    public class DeleteLegalCaseCommandValidator : AbstractValidator<DeleteLegalCaseCommand>
    {
        public DeleteLegalCaseCommandValidator()
        {
            RuleFor(v => v.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
