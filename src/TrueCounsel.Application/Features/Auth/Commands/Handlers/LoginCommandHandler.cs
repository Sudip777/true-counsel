using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Models;
using TrueCounsel.Application.Features.Auth.Commands;
using TrueCounsel.Application.Features.Auth.Dtos;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Application.Features.Auth.Commands.Handlers
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _paswordHasher;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _paswordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        /// <summary> handls login logic and saves tokin </summary>
        public async Task<LoginResponseDto> HandleAsync(LoginCommand command, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(command);
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(command.Email).ConfigureAwait(false);

            if (user is null || !user.IsActive)
                throw new UnauthorizedAccessException("Invalid email or password");

            var isPasswordValid = _paswordHasher.VerifyPassword(command.Password, user.PasswordHash);

            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Invalid email or password");

            var access_token = _jwtTokenService.GenerateAccessToken(user);
            var refresh_token = _jwtTokenService.GenerateRefreshToken();

            // Store Refresh Token
            var refreshTokenEntity = new RefreshToken
            {
                Token = refresh_token,
                UserId = user.Id,
                Expires = DateTime.UtcNow.AddDays(7), 
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshTokenEntity);
            await _unitOfWork.CommitAsync();

            return new LoginResponseDto
            {
                AccessToken = access_token,
                RefreshToken = refresh_token,
                User = _mapper.Map<UserDto>(user)
            };
        }
    }
}
