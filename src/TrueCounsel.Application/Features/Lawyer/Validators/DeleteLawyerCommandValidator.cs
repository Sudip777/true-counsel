using FluentValidation;
using TrueCounsel.Application.Features.Lawyer.Commands;

namespace TrueCounsel.Application.Features.Lawyer.Validators
{
    public class DeleteLawyerCommandValidator : AbstractValidator<DeleteLawyerCommand>
    {
        public DeleteLawyerCommandValidator()
        {
             RuleFor(v => v.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
