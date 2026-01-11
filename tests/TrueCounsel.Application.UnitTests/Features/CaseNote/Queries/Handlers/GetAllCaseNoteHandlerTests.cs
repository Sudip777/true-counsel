using System.Collections.Generic;
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
    public class GetAllCaseNoteHandlerTests
    {
        private readonly Mock<ICaseNoteRepository> _mockRepository;
        private readonly IMapper _mapper;
        private readonly GetAllCaseNoteHandler _handler;

        public GetAllCaseNoteHandlerTests()
        {
            _mockRepository = new Mock<ICaseNoteRepository>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetAllCaseNoteHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_All_CaseNotes()
        {
            // Arrange
            var notes = new List<DomainEntities.CaseNote>
            {
                new DomainEntities.CaseNote { Id = 1, Note = "Note 1", CaseId = 1, AuthorId = 1 },
                new DomainEntities.CaseNote { Id = 2, Note = "Note 2", CaseId = 1, AuthorId = 1 }
            };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(notes);

            var query = new GetAllCaseNoteQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
