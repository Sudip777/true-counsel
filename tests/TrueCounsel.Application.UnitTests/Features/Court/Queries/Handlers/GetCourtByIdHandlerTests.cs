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
    public class GetCourtByIdHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetCourtByIdHandler _handler;

        public GetCourtByIdHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetCourtByIdHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_Court_By_Id_When_Exists()
        {
            // Arrange
            var courtId = 1;
            var court = new DomainEntities.Court { Id = courtId, Name = "Supreme Court" };

            _mockUnitOfWork.Setup(u => u.CourtRepository.GetByIdAsync(courtId))
                .ReturnsAsync(court);

            var query = new GetCourtByIdQuery(courtId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(courtId);
            _mockUnitOfWork.Verify(u => u.CourtRepository.GetByIdAsync(courtId), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Null_When_Court_Does_Not_Exist()
        {
            // Arrange
            var courtId = 1;
            _mockUnitOfWork.Setup(u => u.CourtRepository.GetByIdAsync(courtId))
                .ReturnsAsync((DomainEntities.Court?)null);

            var query = new GetCourtByIdQuery(courtId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
            _mockUnitOfWork.Verify(u => u.CourtRepository.GetByIdAsync(courtId), Times.Once);
        }
    }
}
