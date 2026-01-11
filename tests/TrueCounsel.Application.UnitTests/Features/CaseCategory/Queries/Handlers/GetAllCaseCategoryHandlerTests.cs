using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.CaseCategory.Dtos;
using TrueCounsel.Application.Features.CaseCategory.Queries;
using TrueCounsel.Application.Features.CaseCategory.Queries.Handlers;
using DomainEntities = TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseCategory.Queries.Handlers
{
    public class GetAllCaseCategoryHandlerTests
    {
        private readonly Mock<ICaseCategory> _mockRepository;
        private readonly IMapper _mapper;
        private readonly GetAllCaseCategoryHandler _handler;

        public GetAllCaseCategoryHandlerTests()
        {
            _mockRepository = new Mock<ICaseCategory>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetAllCaseCategoryHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_All_CaseCategories()
        {
            // Arrange
            var categories = new List<DomainEntities.CaseCategory>
            {
                new DomainEntities.CaseCategory { Id = 1, Name = "Category 1", Description = "Desc 1", CaseTypeId = 1 },
                new DomainEntities.CaseCategory { Id = 2, Name = "Category 2", Description = "Desc 2", CaseTypeId = 1 }
            };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(categories);

            var query = new GetAllCaseCategoryQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
