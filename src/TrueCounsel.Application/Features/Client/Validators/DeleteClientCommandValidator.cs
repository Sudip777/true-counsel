using FluentValidation;
using TrueCounsel.Application.Features.Client.Commands;

namespace TrueCounsel.Application.Features.Client.Validators
{
    public class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientCommandValidator()
        {
            RuleFor(v => v.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
