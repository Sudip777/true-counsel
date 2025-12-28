using FluentValidation;
using TrueCounsel.Application.Features.CaseType.Commands;

namespace TrueCounsel.Application.Features.CaseType.Validators
{
    public class CreateCaseTypeCommandValidator : AbstractValidator<CreateCaseTypeCommand>
    {
        public CreateCaseTypeCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters.");

            RuleFor(v => v.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}
