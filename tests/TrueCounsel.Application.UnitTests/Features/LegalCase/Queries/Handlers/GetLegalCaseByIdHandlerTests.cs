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
    public class GetLegalCaseByIdHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetLegalCaseByIdHandler _handler;

        public GetLegalCaseByIdHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetLegalCaseByIdHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_LegalCase_By_Id_When_Exists()
        {
            // Arrange
            var caseId = 1;
            var legalCase = new DomainEntities.LegalCase { Id = caseId, CaseNumber = "CASE-001" };

            _mockUnitOfWork.Setup(u => u.LegalCaseRepository.GetByIdAsync(caseId))
                .ReturnsAsync(legalCase);

            var query = new GetLegalCaseByIdQuery(caseId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(caseId);
            _mockUnitOfWork.Verify(u => u.LegalCaseRepository.GetByIdAsync(caseId), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Null_When_LegalCase_Does_Not_Exist()
        {
            // Arrange
            var caseId = 1;
            _mockUnitOfWork.Setup(u => u.LegalCaseRepository.GetByIdAsync(caseId))
                .ReturnsAsync((DomainEntities.LegalCase?)null);

            var query = new GetLegalCaseByIdQuery(caseId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
            _mockUnitOfWork.Verify(u => u.LegalCaseRepository.GetByIdAsync(caseId), Times.Once);
        }
    }
}
