using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Commands.Handlers;
using FluentAssertions;
using Xunit;
using FluentValidation;
using FluentValidation.Results;

namespace TrueCounsel.Application.UnitTests.Features.Lawyer.Commands.Handlers
{
    public class DeleteLawyerHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IValidator<DeleteLawyerCommand>> _mockValidator;
        private readonly DeleteLawyerHandler _handler;

        public DeleteLawyerHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockValidator = new Mock<IValidator<DeleteLawyerCommand>>();
            _handler = new DeleteLawyerHandler(_mockUnitOfWork.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task Should_Delete_Lawyer_When_Lawyer_Exists()
        {
            // Arrange
            var command = new DeleteLawyerCommand(1);

            _mockValidator.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _mockUnitOfWork.Setup(u => u.LawyerRepository.DeleteAsync(command.Id))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.LawyerRepository.DeleteAsync(command.Id), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Return_False_When_Lawyer_Does_Not_Exist()
        {
            // Arrange
            var command = new DeleteLawyerCommand(99);

            _mockValidator.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _mockUnitOfWork.Setup(u => u.LawyerRepository.DeleteAsync(command.Id))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        }
    }
}
