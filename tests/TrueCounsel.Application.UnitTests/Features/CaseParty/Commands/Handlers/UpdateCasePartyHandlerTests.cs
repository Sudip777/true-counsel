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
using TrueCounsel.Domain.Enums;
using DomainEntities = TrueCounsel.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseParty.Commands.Handlers
{
    public class UpdateCasePartyHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateCasePartyHandler _handler;

        public UpdateCasePartyHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new UpdateCasePartyHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Update_CaseParty_When_Exists()
        {
            // Arrange
            var command = new UpdateCasePartyCommand
            {
                Id = 1,
                Name = "John Updated",
                Role = CasePartyRole.Defendant
            };

            var caseParty = new DomainEntities.CaseParty 
            { 
                Id = 1, 
                Name = "John Doe",
                Role = CasePartyRole.Plaintiff,
                CaseId = 1
            };

            _mockUnitOfWork.Setup(u => u.CasePartyRepository.GetByIdAsync(command.Id))
                .ReturnsAsync(caseParty);

            _mockUnitOfWork.Setup(u => u.CasePartyRepository.UpdateAsync(It.IsAny<DomainEntities.CaseParty>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(command.Name);
            result.Role.Should().Be(command.Role);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_CaseParty_Not_Found()
        {
            // Arrange
            var command = new UpdateCasePartyCommand { Id = 99 };
            
            _mockUnitOfWork.Setup(u => u.CasePartyRepository.GetByIdAsync(command.Id))
                .ReturnsAsync((DomainEntities.CaseParty)null!);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
