using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.LegalCase.Commands;

namespace TrueCounsel.Application.Features.LegalCase.Commands.Handlers
{
    public class DeleteLegalCaseHandler : IRequestHandler<DeleteLegalCaseCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLegalCaseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteLegalCaseCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.LegalCaseRepository.DeleteAsync(request.Id);
            if (result)
            {
                await _unitOfWork.CommitAsync();
            }
            return result;
        }
    }
}
