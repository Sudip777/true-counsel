using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.CaseParty.Dtos;
using TrueCounsel.Application.Features.CaseParty.Queries;
using TrueCounsel.Application.Features.CaseParty.Queries.Handlers;
using DomainEntities = TrueCounsel.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseParty.Queries.Handlers
{
    public class GetCasePartiesByCaseIdHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetCasePartiesByCaseIdHandler _handler;

        public GetCasePartiesByCaseIdHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetCasePartiesByCaseIdHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_Parties_For_Specific_Case()
        {
            // Arrange
            var caseId = 1;
            var parties = new List<DomainEntities.CaseParty>
            {
                new DomainEntities.CaseParty { Id = 1, Name = "Party 1", CaseId = caseId },
                new DomainEntities.CaseParty { Id = 2, Name = "Party 2", CaseId = caseId }
            };

            _mockUnitOfWork.Setup(u => u.CasePartyRepository.GetByCaseIdAsync(caseId))
                .ReturnsAsync(parties);

            var query = new GetCasePartiesByCaseIdQuery(caseId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            _mockUnitOfWork.Verify(u => u.CasePartyRepository.GetByCaseIdAsync(caseId), Times.Once);
        }
    }
}
