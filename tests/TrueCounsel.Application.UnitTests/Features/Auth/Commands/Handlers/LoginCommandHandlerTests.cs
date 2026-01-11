using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.Auth.Commands;
using TrueCounsel.Application.Features.Auth.Commands.Handlers;
using TrueCounsel.Application.Features.Auth.Dtos;
using DomainEntities = TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using FluentAssertions;
using Xunit;
using TrueCounsel.Application.Common.Models;

namespace TrueCounsel.Application.UnitTests.Features.Auth.Commands.Handlers
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly Mock<IJwtTokenService> _mockJwtTokenService;
        private readonly IMapper _mapper;
        private readonly LoginCommandHandler _handler;

        public LoginCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockJwtTokenService = new Mock<IJwtTokenService>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new LoginCommandHandler(
                _mockUnitOfWork.Object, 
                _mockJwtTokenService.Object, 
                _mockPasswordHasher.Object, 
                _mapper);
        }

        [Fact]
        public async Task Should_Return_Tokens_When_Credentials_Are_Valid()
        {
            // Arrange
            var command = new LoginCommand("test@example.com", "Password123");

            var user = new DomainEntities.User 
            { 
                Id = 1, 
                Email = command.Email, 
                PasswordHash = "hashed_password",
                Name = "Test User",
                IsActive = true
            };

            _mockUnitOfWork.Setup(u => u.UserRepository.GetByEmailAsync(command.Email))
                .ReturnsAsync(user);

            _mockPasswordHasher.Setup(p => p.VerifyPassword(command.Password, user.PasswordHash))
                .Returns(true);

            _mockJwtTokenService.Setup(j => j.GenerateAccessToken(user))
                .Returns("access_token");

            _mockJwtTokenService.Setup(j => j.GenerateRefreshToken())
                .Returns("refresh_token");

            _mockUnitOfWork.Setup(u => u.RefreshTokenRepository.AddAsync(It.IsAny<DomainEntities.RefreshToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.AccessToken.Should().Be("access_token");
            result.RefreshToken.Should().Be("refresh_token");
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_UnauthorizedAccessException_When_Password_Is_Incorrect()
        {
            // Arrange
            var command = new LoginCommand("test@example.com", "WrongPassword");
            var user = new DomainEntities.User { Id = 1, Email = command.Email, PasswordHash = "hash", Name = "User", IsActive = true };

            _mockUnitOfWork.Setup(u => u.UserRepository.GetByEmailAsync(command.Email))
                .ReturnsAsync(user);

            _mockPasswordHasher.Setup(p => p.VerifyPassword(command.Password, user.PasswordHash))
                .Returns(false);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("Invalid email or password");
        }

        [Fact]
        public async Task Should_Throw_UnauthorizedAccessException_When_User_Is_Inactive()
        {
            // Arrange
            var command = new LoginCommand("test@example.com", "Password123");
            var user = new DomainEntities.User { Id = 1, Email = command.Email, PasswordHash = "hash", Name = "User", IsActive = false };

            _mockUnitOfWork.Setup(u => u.UserRepository.GetByEmailAsync(command.Email))
                .ReturnsAsync(user);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("Invalid email or password");
        }
    }
}
