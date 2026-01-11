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
    public class GetCasePartyByIdHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetCasePartyByIdHandler _handler;

        public GetCasePartyByIdHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetCasePartyByIdHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_Party_By_Id_When_Exists()
        {
            // Arrange
            var partyId = 1;
            var party = new DomainEntities.CaseParty { Id = partyId, Name = "Party 1", CaseId = 1 };

            _mockUnitOfWork.Setup(u => u.CasePartyRepository.GetByIdAsync(partyId))
                .ReturnsAsync(party);

            var query = new GetCasePartyByIdQuery(partyId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(partyId);
            _mockUnitOfWork.Verify(u => u.CasePartyRepository.GetByIdAsync(partyId), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Null_When_Party_Does_Not_Exist()
        {
            // Arrange
            var partyId = 1;
            _mockUnitOfWork.Setup(u => u.CasePartyRepository.GetByIdAsync(partyId))
                .ReturnsAsync((DomainEntities.CaseParty?)null);

            var query = new GetCasePartyByIdQuery(partyId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
            _mockUnitOfWork.Verify(u => u.CasePartyRepository.GetByIdAsync(partyId), Times.Once);
        }
    }
}
