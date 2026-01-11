using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.LegalCase.Dtos;
using TrueCounsel.Application.Features.LegalCase.Queries;
using TrueCounsel.Application.Features.LegalCase.Queries.Handlers;
using DomainEntities = TrueCounsel.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.LegalCase.Queries.Handlers
{
    public class GetAllLegalCasesHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetAllLegalCasesHandler _handler;

        public GetAllLegalCasesHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetAllLegalCasesHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_All_LegalCases()
        {
            // Arrange
            var cases = new List<DomainEntities.LegalCase>
            {
                new DomainEntities.LegalCase { Id = 1, CaseNumber = "CASE-001" },
                new DomainEntities.LegalCase { Id = 2, CaseNumber = "CASE-002" }
            };

            _mockUnitOfWork.Setup(u => u.LegalCaseRepository.GetAllAsync())
                .ReturnsAsync(cases);

            var query = new GetAllLegalCasesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            _mockUnitOfWork.Verify(u => u.LegalCaseRepository.GetAllAsync(), Times.Once);
        }
    }
}
