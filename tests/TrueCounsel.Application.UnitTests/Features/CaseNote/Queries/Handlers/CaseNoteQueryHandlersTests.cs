using System;
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
    public class CaseNoteQueryHandlersTests
    {
        private readonly Mock<ICaseNoteRepository> _mockRepository;
        private readonly IMapper _mapper;

        public CaseNoteQueryHandlersTests()
        {
            _mockRepository = new Mock<ICaseNoteRepository>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetAllCaseNoteHandler_Should_Return_All_Notes()
        {
            // Arrange
            var notes = new List<DomainEntities.CaseNote>
            {
                new DomainEntities.CaseNote { Id = 1, Note = "N1" },
                new DomainEntities.CaseNote { Id = 2, Note = "N2" }
            };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(notes);

            var handler = new GetAllCaseNoteHandler(_mockRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetAllCaseNoteQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetCaseNoteByIdHandler_Should_Return_Note_When_Exists()
        {
            // Arrange
            var note = new DomainEntities.CaseNote { Id = 1, Note = "N1" };

            _mockRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(note);

            var handler = new GetCaseNoteByIdHandler(_mockRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetCaseNoteByIdQuery(1), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }
    }
}
