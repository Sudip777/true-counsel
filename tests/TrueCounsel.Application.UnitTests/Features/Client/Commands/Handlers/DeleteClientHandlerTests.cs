using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Client.Commands;
using TrueCounsel.Application.Features.Client.Commands.Handlers;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Client.Commands.Handlers
{
    public class DeleteClientHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DeleteClientHandler _handler;

        public DeleteClientHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new DeleteClientHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Should_Delete_Client_When_Client_Exists()
        {
            // Arrange
            var command = new DeleteClientCommand(1);

            _mockUnitOfWork.Setup(u => u.ClientRepository.DeleteAsync(command.Id))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.ClientRepository.DeleteAsync(command.Id), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Return_False_When_Client_Does_Not_Exist()
        {
            // Arrange
            var command = new DeleteClientCommand(99);

            _mockUnitOfWork.Setup(u => u.ClientRepository.DeleteAsync(command.Id))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        }
    }
}
