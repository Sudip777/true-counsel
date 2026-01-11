using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.Auth.Commands;
using TrueCounsel.Application.Features.Auth.Commands.Handlers;
using TrueCounsel.Application.Common.Models;
using DomainEntities = TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Auth.Commands.Handlers
{
    public class RegisterAuthCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly IMapper _mapper;
        private readonly RegisterAuthCommandHandler _handler;

        public RegisterAuthCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new RegisterAuthCommandHandler(
                _mockUnitOfWork.Object, 
                _mockPasswordHasher.Object, 
                _mapper);
        }

        [Fact]
        public async Task Should_Register_User_When_Email_Is_Unique()
        {
            // Arrange
            var command = new RegisterAuthCommand("New User", "new@example.com", "Password123");

            _mockUnitOfWork.Setup(u => u.UserRepository.GetByEmailAsync(command.Email))
                .ReturnsAsync((DomainEntities.User)null!);

            _mockPasswordHasher.Setup(p => p.HashPassword(command.Password))
                .Returns("hashed_password");

            _mockUnitOfWork.Setup(u => u.UserRepository.AddAsync(It.IsAny<DomainEntities.User>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(command.Email);
            _mockUnitOfWork.Verify(u => u.UserRepository.AddAsync(It.IsAny<DomainEntities.User>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_InvalidOperationException_When_Email_Already_Exists()
        {
            // Arrange
            var command = new RegisterAuthCommand("User", "existing@example.com", "Pass");
            var existingUser = new DomainEntities.User { Id = 1, Email = command.Email, Name = "Existing", PasswordHash = "hash" };

            _mockUnitOfWork.Setup(u => u.UserRepository.GetByEmailAsync(command.Email))
                .ReturnsAsync(existingUser);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Email is already in use.");
        }
    }
}
