using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TrueCounsel.Application.Features.CaseNote.Commands;
using TrueCounsel.Application.Features.CaseNote.Commands.Handlers;
using TrueCounsel.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseNote.Commands.Handlers
{
    public class DeleteCaseNoteHandlerTests
    {
        private readonly Mock<ICaseNoteRepository> _mockRepository;
        private readonly DeleteCaseNoteHandler _handler;

        public DeleteCaseNoteHandlerTests()
        {
            _mockRepository = new Mock<ICaseNoteRepository>();
            _handler = new DeleteCaseNoteHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Should_Delete_CaseNote_When_Exists()
        {
            // Arrange
            var command = new DeleteCaseNoteCommand(1);

            _mockRepository.Setup(r => r.DeleteAsync(command.Id))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _mockRepository.Verify(r => r.DeleteAsync(command.Id), Times.Once);
        }

        [Fact]
        public async Task Should_Return_False_When_CaseNote_Does_Not_Exist()
        {
            // Arrange
            var command = new DeleteCaseNoteCommand(99);

            _mockRepository.Setup(r => r.DeleteAsync(command.Id))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }
    }
}
