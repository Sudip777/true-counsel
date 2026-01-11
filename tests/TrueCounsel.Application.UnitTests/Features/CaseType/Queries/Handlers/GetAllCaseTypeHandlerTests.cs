using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.CaseType.Dtos;
using TrueCounsel.Application.Features.CaseType.Queries;
using TrueCounsel.Application.Features.CaseType.Queries.Handlers;
using DomainEntities = TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseType.Queries.Handlers
{
    public class GetAllCaseTypeHandlerTests
    {
        private readonly Mock<ICaseTypeRepository> _mockRepository;
        private readonly IMapper _mapper;
        private readonly GetAllCaseTypeHandler _handler;

        public GetAllCaseTypeHandlerTests()
        {
            _mockRepository = new Mock<ICaseTypeRepository>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetAllCaseTypeHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_All_CaseTypes()
        {
            // Arrange
            var types = new List<DomainEntities.CaseType>
            {
                new DomainEntities.CaseType { Id = 1, Name = "Type 1", Description = "Desc 1" },
                new DomainEntities.CaseType { Id = 2, Name = "Type 2", Description = "Desc 2" }
            };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(types);

            var query = new GetAllCaseTypeQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
