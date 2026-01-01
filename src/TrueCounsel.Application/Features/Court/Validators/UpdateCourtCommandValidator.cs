using FluentValidation;
using TrueCounsel.Application.Features.Court.Commands;

namespace TrueCounsel.Application.Features.Court.Validators
{
    /// <summary>
    /// Validator for UpdateCourtCommand.
    /// Ensures that the command data meets business rules before processing.
    /// </summary>
    public class UpdateCourtCommandValidator : AbstractValidator<UpdateCourtCommand>
    {
        public UpdateCourtCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Court ID is required.")
                .GreaterThan(0).WithMessage("Court ID must be greater than 0.");

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Court name is required.")
                .MaximumLength(200).WithMessage("Court name must not exceed 200 characters.");

            RuleFor(v => v.Address)
                .MaximumLength(250).WithMessage("Address must not exceed 250 characters.");

            RuleFor(v => v.City)
                .MaximumLength(100).WithMessage("City must not exceed 100 characters.");

            RuleFor(v => v.State)
                .MaximumLength(50).WithMessage("State must not exceed 50 characters.");
        }
    }
}
