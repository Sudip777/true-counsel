using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.Court.Dtos;
using TrueCounsel.Application.Features.Court.Queries;
using TrueCounsel.Application.Features.Court.Queries.Handlers;
using DomainEntities = TrueCounsel.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Court.Queries.Handlers
{
    public class GetAllCourtsHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetAllCourtsHandler _handler;

        public GetAllCourtsHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetAllCourtsHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_All_Courts()
        {
            // Arrange
            var courts = new List<DomainEntities.Court>
            {
                new DomainEntities.Court { Id = 1, Name = "Supreme Court" },
                new DomainEntities.Court { Id = 2, Name = "High Court" }
            };

            _mockUnitOfWork.Setup(u => u.CourtRepository.GetAllAsync())
                .ReturnsAsync(courts);

            var query = new GetAllCourtsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            _mockUnitOfWork.Verify(u => u.CourtRepository.GetAllAsync(), Times.Once);
        }
    }
}
