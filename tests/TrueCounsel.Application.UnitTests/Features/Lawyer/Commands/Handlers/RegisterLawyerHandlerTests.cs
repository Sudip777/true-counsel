using System;
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
    public class RegisterLawyerHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly Mock<IValidator<RegisterLawyerCommand>> _mockValidator;
        private readonly RegisterLawyerHandler _handler;

        public RegisterLawyerHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockValidator = new Mock<IValidator<RegisterLawyerCommand>>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new RegisterLawyerHandler(_mockUnitOfWork.Object, _mapper, _mockValidator.Object);
        }

        [Fact]
        public async Task Should_Register_Lawyer_When_Request_Is_Valid()
        {
            // Arrange
            var command = new RegisterLawyerCommand
            {
                UserId = 1,
                BarNumber = "BAR123",
                Specialization = "Criminal Law",
                YearsOfExperience = 5
            };

            var user = new DomainEntities.User 
            { 
                Id = 1, 
                Name = "Lawyer User", 
                Email = "lawyer@example.com", 
                PasswordHash = "hash" 
            };

            _mockValidator.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _mockUnitOfWork.Setup(u => u.UserRepository.GetByIdAsync(command.UserId))
                .ReturnsAsync(user);

            _mockUnitOfWork.Setup(u => u.LawyerRepository.AddAsync(It.IsAny<DomainEntities.Lawyer>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Specialization.Should().Be(command.Specialization);
            _mockUnitOfWork.Verify(u => u.LawyerRepository.AddAsync(It.IsAny<DomainEntities.Lawyer>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_ArgumentException_When_User_Not_Found()
        {
            // Arrange
            var command = new RegisterLawyerCommand { UserId = 99, BarNumber = "BAR999" };
            
            _mockValidator.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _mockUnitOfWork.Setup(u => u.UserRepository.GetByIdAsync(command.UserId))
                .ReturnsAsync((DomainEntities.User)null!);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*User with id 99 not found.*");
        }
    }
}
