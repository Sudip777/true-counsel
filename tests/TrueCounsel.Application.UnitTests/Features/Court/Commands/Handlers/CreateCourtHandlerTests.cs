using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.Court.Commands;
using TrueCounsel.Application.Features.Court.Commands.Handlers;
using TrueCounsel.Application.Features.Court.Dtos;
using DomainEntities = TrueCounsel.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Court.Commands.Handlers
{
    public class CreateCourtHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly CreateCourtHandler _handler;

        public CreateCourtHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new CreateCourtHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Create_Court_When_Command_Is_Valid()
        {
            // Arrange
            var command = new CreateCourtCommand
            {
                Name = "Supreme Court",
                Address = "123 Main St",
                City = "Kathmandu",
                State = "Bagmati",
                CourtTypeId = 1
            };

            var court = new DomainEntities.Court 
            { 
                Id = 1, 
                Name = command.Name,
                Address = command.Address,
                City = command.City,
                State = command.State
            };

            _mockUnitOfWork.Setup(u => u.CourtRepository.AddAsync(It.IsAny<DomainEntities.Court>()))
                .Callback<DomainEntities.Court>(c => c.Id = 1)
                .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(u => u.CourtRepository.GetByIdAsync(1))
                .ReturnsAsync(court);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(command.Name);
            _mockUnitOfWork.Verify(u => u.CourtRepository.AddAsync(It.IsAny<DomainEntities.Court>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
