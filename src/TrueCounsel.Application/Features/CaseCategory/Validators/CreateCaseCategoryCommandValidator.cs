using FluentValidation;
using TrueCounsel.Application.Features.CaseCategory.Commands;

namespace TrueCounsel.Application.Features.CaseCategory.Validators
{
    public class CreateCaseCategoryCommandValidator : AbstractValidator<CreateCaseCategoryCommand>
    {
        public CreateCaseCategoryCommandValidator()
        {
            RuleFor(v => v.CaseTypeId)
                .NotEmpty().WithMessage("CaseTypeId is required.")
                .GreaterThan(0).WithMessage("CaseTypeId must be greater than 0.");

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters.");

            RuleFor(v => v.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}
