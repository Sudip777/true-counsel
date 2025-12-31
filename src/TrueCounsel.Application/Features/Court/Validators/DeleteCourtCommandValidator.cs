using FluentValidation;
using TrueCounsel.Application.Features.Court.Commands;

namespace TrueCounsel.Application.Features.Court.Validators
{
    public class DeleteCourtCommandValidator : AbstractValidator<DeleteCourtCommand>
    {
        public DeleteCourtCommandValidator()
        {
            RuleFor(v => v.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
