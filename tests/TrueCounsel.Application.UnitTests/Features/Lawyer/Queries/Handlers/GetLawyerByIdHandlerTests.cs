using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.Lawyer.Dtos;
using TrueCounsel.Application.Features.Lawyer.Queries;
using TrueCounsel.Application.Features.Lawyer.Queries.Handlers;
using DomainEntities = TrueCounsel.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Lawyer.Queries.Handlers
{
    public class GetLawyerByIdHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetLawyerByIdHandler _handler;

        public GetLawyerByIdHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetLawyerByIdHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_Lawyer_By_Id_When_Exists()
        {
            // Arrange
            var lawyerId = 1;
            var lawyer = new DomainEntities.Lawyer { Id = lawyerId, BarNumber = "L123", HourlyRate = 100 };

            _mockUnitOfWork.Setup(u => u.LawyerRepository.GetByIdAsync(lawyerId))
                .ReturnsAsync(lawyer);

            var query = new GetLawyerByIdQuery(lawyerId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(lawyerId);
            _mockUnitOfWork.Verify(u => u.LawyerRepository.GetByIdAsync(lawyerId), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Null_When_Lawyer_Does_Not_Exist()
        {
            // Arrange
            var lawyerId = 1;
            _mockUnitOfWork.Setup(u => u.LawyerRepository.GetByIdAsync(lawyerId))
                .ReturnsAsync((DomainEntities.Lawyer?)null);

            var query = new GetLawyerByIdQuery(lawyerId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
            _mockUnitOfWork.Verify(u => u.LawyerRepository.GetByIdAsync(lawyerId), Times.Once);
        }
    }
}
