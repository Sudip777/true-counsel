using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Commands.Handlers;
using TrueCounsel.Application.Features.Lawyer.Dtos;
using DomainEntities = TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using FluentAssertions;
using Xunit;
using FluentValidation;
using FluentValidation.Results;

namespace TrueCounsel.Application.UnitTests.Features.Lawyer.Commands.Handlers
{
    public class UpdateLawyerHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly Mock<IValidator<UpdateLawyerCommand>> _mockValidator;
        private readonly UpdateLawyerHandler _handler;

        public UpdateLawyerHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockValidator = new Mock<IValidator<UpdateLawyerCommand>>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new UpdateLawyerHandler(_mockUnitOfWork.Object, _mapper, _mockValidator.Object);
        }

        [Fact]
        public async Task Should_Update_Lawyer_When_Request_Is_Valid()
        {
            // Arrange
            var command = new UpdateLawyerCommand
            {
                Id = 1,
                Specialization = "Updated Specialization",
                YearsOfExperience = 10
            };

            var lawyer = new DomainEntities.Lawyer 
            { 
                Id = 1, 
                UserId = 1,
                BarNumber = "BAR123",
                Specialization = "Old Specialization",
                YearsOfExperience = 5,
                HourlyRate = 100
            };

            _mockValidator.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _mockUnitOfWork.Setup(u => u.LawyerRepository.GetByIdAsync(command.Id))
                .ReturnsAsync(lawyer);

            _mockUnitOfWork.Setup(u => u.LawyerRepository.UpdateAsync(It.IsAny<DomainEntities.Lawyer>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Specialization.Should().Be(command.Specialization);
            result.YearsOfExperience.Should().Be(command.YearsOfExperience);
            _mockUnitOfWork.Verify(u => u.LawyerRepository.UpdateAsync(It.IsAny<DomainEntities.Lawyer>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_KeyNotFoundException_When_Lawyer_DoesNotExist()
        {
            // Arrange
            var command = new UpdateLawyerCommand { Id = 99 };
            
            _mockValidator.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _mockUnitOfWork.Setup(u => u.LawyerRepository.GetByIdAsync(command.Id))
                .ReturnsAsync((DomainEntities.Lawyer)null!);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("*Lawyer with id 99 not found*");
        }
    }
}
