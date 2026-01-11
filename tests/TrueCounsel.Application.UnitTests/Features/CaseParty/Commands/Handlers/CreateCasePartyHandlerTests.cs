using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.CaseParty.Commands;
using TrueCounsel.Application.Features.CaseParty.Commands.Handlers;
using TrueCounsel.Application.Features.CaseParty.Dtos;
using DomainEntities = TrueCounsel.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseParty.Commands.Handlers
{
    public class CreateCasePartyHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly CreateCasePartyHandler _handler;

        public CreateCasePartyHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new CreateCasePartyHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Create_CaseParty_When_Command_Is_Valid()
        {
            // Arrange
            var command = new CreateCasePartyCommand
            {
                CaseId = 1,
                Name = "John Doe",
                Role = TrueCounsel.Domain.Enums.CasePartyRole.Plaintiff,
                ContactEmail = "john@example.com"
            };

            var caseParty = new DomainEntities.CaseParty 
            { 
                Id = 1, 
                CaseId = 1,
                Name = "John Doe",
                Role = TrueCounsel.Domain.Enums.CasePartyRole.Plaintiff
            };

            _mockUnitOfWork.Setup(u => u.CasePartyRepository.AddAsync(It.IsAny<DomainEntities.CaseParty>()))
                .Callback<DomainEntities.CaseParty>(cp => cp.Id = 1)
                .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(u => u.CasePartyRepository.GetByIdAsync(1))
                .ReturnsAsync(caseParty);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(command.Name);
            _mockUnitOfWork.Verify(u => u.CasePartyRepository.AddAsync(It.IsAny<DomainEntities.CaseParty>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
