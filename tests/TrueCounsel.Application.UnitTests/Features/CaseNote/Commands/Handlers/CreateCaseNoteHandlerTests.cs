using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.CaseNote.Commands;
using TrueCounsel.Application.Features.CaseNote.Commands.Handlers;
using TrueCounsel.Application.Features.CaseNote.Dtos;
using DomainEntities = TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseNote.Commands.Handlers
{
    public class CreateCaseNoteHandlerTests
    {
        private readonly Mock<ICaseNoteRepository> _mockRepository;
        private readonly IMapper _mapper;
        private readonly CreateCaseNoteHandler _handler;

        public CreateCaseNoteHandlerTests()
        {
            _mockRepository = new Mock<ICaseNoteRepository>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new CreateCaseNoteHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Should_Create_CaseNote_When_Command_Is_Valid()
        {
            // Arrange
            var command = new CreateCaseNoteCommand
            {
                CaseId = 1,
                Note = "Test Note Content"
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<DomainEntities.CaseNote>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Note.Should().Be(command.Note);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<DomainEntities.CaseNote>()), Times.Once);
        }
    }
}
