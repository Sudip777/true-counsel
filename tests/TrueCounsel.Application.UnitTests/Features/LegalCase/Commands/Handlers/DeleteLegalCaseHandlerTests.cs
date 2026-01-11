using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.LegalCase.Commands;
using TrueCounsel.Application.Features.LegalCase.Commands.Handlers;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.LegalCase.Commands.Handlers
{
    public class DeleteLegalCaseHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DeleteLegalCaseHandler _handler;

        public DeleteLegalCaseHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new DeleteLegalCaseHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Should_Delete_LegalCase_When_Exists()
        {
            // Arrange
            var command = new DeleteLegalCaseCommand(1);

            _mockUnitOfWork.Setup(u => u.LegalCaseRepository.DeleteAsync(command.Id))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.LegalCaseRepository.DeleteAsync(command.Id), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Return_False_When_LegalCase_Does_Not_Exist()
        {
            // Arrange
            var command = new DeleteLegalCaseCommand(99);

            _mockUnitOfWork.Setup(u => u.LegalCaseRepository.DeleteAsync(command.Id))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        }
    }
}
