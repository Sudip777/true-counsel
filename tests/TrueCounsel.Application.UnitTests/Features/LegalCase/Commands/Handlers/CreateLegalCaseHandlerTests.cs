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
using TrueCounsel.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.LegalCase.Commands.Handlers
{
    public class CreateLegalCaseHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly CreateLegalCaseHandler _handler;

        public CreateLegalCaseHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new CreateLegalCaseHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Create_LegalCase_When_Command_Is_Valid()
        {
            // Arrange
            var command = new CreateLegalCaseCommand
            {
                Title = "Test Case",
                CaseNumber = "CAS-001",
                ClientId = 1,
                LawyerId = 1,
                CaseTypeId = 1
            };

            var legalCase = new DomainEntities.LegalCase 
            { 
                Id = 1, 
                Title = "Test Case",
                CaseNumber = "CAS-001",
                ClientId = 1,
                LawyerId = 1,
                CaseTypeId = 1
            };

            _mockUnitOfWork.Setup(u => u.LegalCaseRepository.AddAsync(It.IsAny<DomainEntities.LegalCase>()))
                .Callback<DomainEntities.LegalCase>(lc => lc.Id = 1)
                .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(u => u.LegalCaseRepository.GetByIdAsync(1))
                .ReturnsAsync(legalCase);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(command.Title);
            _mockUnitOfWork.Verify(u => u.LegalCaseRepository.AddAsync(It.IsAny<DomainEntities.LegalCase>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
