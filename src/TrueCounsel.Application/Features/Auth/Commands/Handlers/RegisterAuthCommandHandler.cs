using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Application.Features.Auth.Commands.Handlers
{
    public class RegisterAuthCommandHandler
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;


        public RegisterAuthCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher password)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = password;
        }

        public async Task RegisterAsync(RegisterAuthCommand command, IPasswordHasher passwordHasher, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(command);


            var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(command.Email).ConfigureAwait(false);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email is already in use");
            }


            var newUser = new User
            {
                Name = command.Name,
                Email = command.Email,
                PasswordHash = _passwordHasher.HashPassword(command.Password),


            };
            await _unitOfWork.UserRepository.AddAsync(newUser).ConfigureAwait(false);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);                                                                                                                                                                          
        }                                                                                       
    }
}
