using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.CaseParty.Commands;

namespace TrueCounsel.Application.Features.CaseParty.Commands.Handlers
{
    public class DeleteCasePartyHandler : IRequestHandler<DeleteCasePartyCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCasePartyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCasePartyCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            
            var result = await _unitOfWork.CasePartyRepository.DeleteAsync(request.Id);
            if (!result) return false;

            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
