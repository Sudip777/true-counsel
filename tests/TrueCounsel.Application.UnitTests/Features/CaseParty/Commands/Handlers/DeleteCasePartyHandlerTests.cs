using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.CaseParty.Commands;
using TrueCounsel.Application.Features.CaseParty.Commands.Handlers;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseParty.Commands.Handlers
{
    public class DeleteCasePartyHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DeleteCasePartyHandler _handler;

        public DeleteCasePartyHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new DeleteCasePartyHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Should_Delete_CaseParty_When_Exists()
        {
            // Arrange
            var command = new DeleteCasePartyCommand(1);

            _mockUnitOfWork.Setup(u => u.CasePartyRepository.DeleteAsync(command.Id))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Return_False_When_CaseParty_Does_Not_Exist()
        {
            // Arrange
            var command = new DeleteCasePartyCommand(99);

            _mockUnitOfWork.Setup(u => u.CasePartyRepository.DeleteAsync(command.Id))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        }
    }
}
