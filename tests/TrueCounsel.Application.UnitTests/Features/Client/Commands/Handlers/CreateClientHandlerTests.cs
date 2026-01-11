using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.Client.Commands;
using TrueCounsel.Application.Features.Client.Commands.Handlers;
using TrueCounsel.Application.Features.Client.Dtos;
using DomainEntities = TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Client.Commands.Handlers
{
    public class CreateClientHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly CreateClientHandler _handler;

        public CreateClientHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new CreateClientHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Create_Client_When_User_Exists()
        {
            // Arrange
            var command = new CreateClientCommand
            {
                UserId = 1,
                Phone = "1234567890",
                Address = "123 Street",
                DateOfBirth = DateTime.UtcNow.AddYears(-20)
            };

            var user = new DomainEntities.User 
            { 
                Id = 1, 
                Name = "Test User", 
                Email = "test@example.com", 
                PasswordHash = "hashedpassword" 
            };
            var client = new DomainEntities.Client { Id = 1, UserId = 1 };

            _mockUnitOfWork.Setup(u => u.UserRepository.GetByIdAsync(command.UserId))
                .ReturnsAsync(user);

            _mockUnitOfWork.Setup(u => u.ClientRepository.AddAsync(It.IsAny<DomainEntities.Client>()))
                .Callback<DomainEntities.Client>(c => c.Id = 1)
                .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(u => u.ClientRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(client);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.UserId.Should().Be(command.UserId);
            _mockUnitOfWork.Verify(u => u.ClientRepository.AddAsync(It.IsAny<DomainEntities.Client>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_User_Does_Not_Exist()
        {
            // Arrange
            var command = new CreateClientCommand { UserId = 99 };

            _mockUnitOfWork.Setup(u => u.UserRepository.GetByIdAsync(command.UserId))
                .ReturnsAsync((DomainEntities.User)null!);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("User with id 99 not found.");
        }
    }
}
