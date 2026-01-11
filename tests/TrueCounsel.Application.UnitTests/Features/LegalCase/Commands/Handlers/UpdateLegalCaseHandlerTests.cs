using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.LegalCase.Commands;
using TrueCounsel.Application.Features.LegalCase.Commands.Handlers;
using TrueCounsel.Application.Features.LegalCase.Dtos;
using DomainEntities = TrueCounsel.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.LegalCase.Commands.Handlers
{
    public class UpdateLegalCaseHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateLegalCaseHandler _handler;

        public UpdateLegalCaseHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new UpdateLegalCaseHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Update_LegalCase_When_Exists()
        {
            // Arrange
            var command = new UpdateLegalCaseCommand
            {
                Id = 1,
                Title = "Updated Title"
            };

            var legalCase = new DomainEntities.LegalCase 
            { 
                Id = 1, 
                Title = "Old Title",
                CaseNumber = "CAS-001",
                ClientId = 1,
                LawyerId = 1,
                CaseTypeId = 1
            };

            _mockUnitOfWork.Setup(u => u.LegalCaseRepository.GetByIdAsync(command.Id))
                .ReturnsAsync(legalCase);

            _mockUnitOfWork.Setup(u => u.LegalCaseRepository.UpdateAsync(It.IsAny<DomainEntities.LegalCase>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(command.Title);
            _mockUnitOfWork.Verify(u => u.LegalCaseRepository.UpdateAsync(It.IsAny<DomainEntities.LegalCase>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Null_When_LegalCase_Not_Found()
        {
            // Arrange
            var command = new UpdateLegalCaseCommand { Id = 99 };
            
            _mockUnitOfWork.Setup(u => u.LegalCaseRepository.GetByIdAsync(command.Id))
                .ReturnsAsync((DomainEntities.LegalCase)null!);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
