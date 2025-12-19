using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Models;
using TrueCounsel.Application.Features.Auth.Dtos;

namespace TrueCounsel.Application.Features.Auth.Commands.Handlers
{
    public class LoginCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _paswordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _paswordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;            
        }

        public async Task<LoginResponseDto> HandleAsyncLogin(LoginCommand command,
            CancellationToken cancellationToken = default)
        {
            // Fetch User

            ArgumentNullException.ThrowIfNull(command);
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(command.Email).ConfigureAwait(false);

            if (user is null || !user.IsActive)
                throw new UnauthorizedAccessException("Invalid email or password");
            // Verify Password


            var isPasswordValid = _paswordHasher
             .VerifyPassword(command.Password, user.PasswordHash);

            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Invalid email or password");
            /// Generate Tokens 
            ///   // 3️⃣ Generate tokens
            var access_token = _jwtTokenService.GenerateAccessToken(user);
            var refresh_token = _jwtTokenService.GenerateRefreshToken();

            /// Return Response

            return new LoginResponseDto             {
                AccessToken = access_token,
                RefreshToken = refresh_token,
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    ProfilePhotoUrl = user.ProfilePhotoUrl,
                    Role = user.Role
                }
            };
        }



    }
}
