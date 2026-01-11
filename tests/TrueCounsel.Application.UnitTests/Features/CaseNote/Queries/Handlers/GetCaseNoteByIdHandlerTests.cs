using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.CaseNote.Dtos;
using TrueCounsel.Application.Features.CaseNote.Queries;
using TrueCounsel.Application.Features.CaseNote.Queries.Handlers;
using DomainEntities = TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseNote.Queries.Handlers
{
    public class GetCaseNoteByIdHandlerTests
    {
        private readonly Mock<ICaseNoteRepository> _mockRepository;
        private readonly IMapper _mapper;
        private readonly GetCaseNoteByIdHandler _handler;

        public GetCaseNoteByIdHandlerTests()
        {
            _mockRepository = new Mock<ICaseNoteRepository>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetCaseNoteByIdHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_CaseNote_By_Id_When_Exists()
        {
            // Arrange
            var noteId = 1;
            var note = new DomainEntities.CaseNote { Id = noteId, Note = "Note 1", CaseId = 1, AuthorId = 1 };

            _mockRepository.Setup(r => r.GetByIdAsync(noteId))
                .ReturnsAsync(note);

            var query = new GetCaseNoteByIdQuery(noteId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(noteId);
            _mockRepository.Verify(r => r.GetByIdAsync(noteId), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Null_When_CaseNote_Does_Not_Exist()
        {
            // Arrange
            var noteId = 1;
            _mockRepository.Setup(r => r.GetByIdAsync(noteId))
                .ReturnsAsync((DomainEntities.CaseNote?)null);

            var query = new GetCaseNoteByIdQuery(noteId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
            _mockRepository.Verify(r => r.GetByIdAsync(noteId), Times.Once);
        }
    }
}
