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
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Client.Commands.Handlers
{
    public class UpdateClientHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateClientHandler _handler;

        public UpdateClientHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new UpdateClientHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Update_Client_When_Request_Is_Valid()
        {
            // Arrange
            var command = new UpdateClientCommand
            {
                Id = 1,
                Phone = "+123456789",
                Address = "New Address"
            };

            var client = new DomainEntities.Client 
            { 
                Id = 1, 
                UserId = 1,
                Phone = "Old Phone",
                Address = "Old Address"
            };

            _mockUnitOfWork.Setup(u => u.ClientRepository.GetByIdAsync(command.Id))
                .ReturnsAsync(client);

            _mockUnitOfWork.Setup(u => u.ClientRepository.UpdateAsync(It.IsAny<DomainEntities.Client>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Phone.Should().Be(command.Phone);
            result.Address.Should().Be(command.Address);
            _mockUnitOfWork.Verify(u => u.ClientRepository.UpdateAsync(It.IsAny<DomainEntities.Client>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Client_Not_Found()
        {
            // Arrange
            var command = new UpdateClientCommand { Id = 99 };
            
            _mockUnitOfWork.Setup(u => u.ClientRepository.GetByIdAsync(command.Id))
                .ReturnsAsync((DomainEntities.Client)null!);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Client with id 99 not found");
        }
    }
}
