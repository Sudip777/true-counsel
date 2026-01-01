using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Client.Commands;

namespace TrueCounsel.Application.Features.Client.Commands.Handlers
{
    public class DeleteClientHandler : IRequestHandler<DeleteClientCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClientHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }    

        public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            
            var result = await _unitOfWork.ClientRepository.DeleteAsync(request.Id);
            if (!result) return false;

            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
