using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Court.Commands;
using TrueCounsel.Application.Features.Court.Commands.Handlers;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Court.Commands.Handlers
{
    public class DeleteCourtHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DeleteCourtHandler _handler;

        public DeleteCourtHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new DeleteCourtHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Should_Delete_Court_When_Exists()
        {
            // Arrange
            var command = new DeleteCourtCommand(1);

            _mockUnitOfWork.Setup(u => u.CourtRepository.DeleteAsync(command.Id))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Return_False_When_Court_Does_Not_Exist()
        {
            // Arrange
            var command = new DeleteCourtCommand(99);

            _mockUnitOfWork.Setup(u => u.CourtRepository.DeleteAsync(command.Id))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        }
    }
}
