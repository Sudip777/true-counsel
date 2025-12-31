using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Lawyer.Commands;

namespace TrueCounsel.Application.Features.Lawyer.Commands.Handlers
{
    public class DeleteLawyerHandler : ICommandHandler<DeleteLawyerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FluentValidation.IValidator<DeleteLawyerCommand> _validator;

        public DeleteLawyerHandler(IUnitOfWork unitOfWork, FluentValidation.IValidator<DeleteLawyerCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        /// <summary> deletz lawer by id </summary>
        public async Task<bool> HandleAsync(DeleteLawyerCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null) return false;

            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                 throw new TrueCounsel.Application.Common.Exceptions.AppValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.LawyerRepository.DeleteAsync(command.Id);
            if (result)
            {
                await _unitOfWork.CommitAsync();
            }
            return result;
        }
    }
}
