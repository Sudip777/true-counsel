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
    public class UpdateCourtHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateCourtHandler _handler;

        public UpdateCourtHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new UpdateCourtHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Update_Court_When_Exists()
        {
            // Arrange
            var command = new UpdateCourtCommand
            {
                Id = 1,
                Name = "Supreme Court Updated",
                City = "Lalitpur"
            };

            var court = new DomainEntities.Court 
            { 
                Id = 1, 
                Name = "Old Court",
                City = "Kathmandu"
            };

            _mockUnitOfWork.Setup(u => u.CourtRepository.GetByIdAsync(command.Id))
                .ReturnsAsync(court);

            _mockUnitOfWork.Setup(u => u.CourtRepository.UpdateAsync(It.IsAny<DomainEntities.Court>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(command.Name);
            result.City.Should().Be(command.City);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Court_Not_Found()
        {
            // Arrange
            var command = new UpdateCourtCommand { Id = 99, Name = "Test" };
            
            _mockUnitOfWork.Setup(u => u.CourtRepository.GetByIdAsync(command.Id))
                .ReturnsAsync((DomainEntities.Court)null!);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
