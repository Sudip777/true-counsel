using FluentValidation;
using TrueCounsel.Application.Features.CaseNote.Commands;

namespace TrueCounsel.Application.Features.CaseNote.Validators
{
    public class DeleteCaseNoteCommandValidator : AbstractValidator<DeleteCaseNoteCommand>
    {
        public DeleteCaseNoteCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
