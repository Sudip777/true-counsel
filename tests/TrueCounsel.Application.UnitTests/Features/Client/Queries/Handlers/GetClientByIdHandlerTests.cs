using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.Client.Dtos;
using TrueCounsel.Application.Features.Client.Queries;
using TrueCounsel.Application.Features.Client.Queries.Handlers;
using DomainEntities = TrueCounsel.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Client.Queries.Handlers
{
    public class GetClientByIdHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetClientByIdHandler _handler;

        public GetClientByIdHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetClientByIdHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_Client_By_Id_When_Exists()
        {
            // Arrange
            var clientId = 1;
            var client = new DomainEntities.Client { Id = clientId, Phone = "1234567890" };

            _mockUnitOfWork.Setup(u => u.ClientRepository.GetByIdAsync(clientId))
                .ReturnsAsync(client);

            var query = new GetClientByIdQuery(clientId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(clientId);
            _mockUnitOfWork.Verify(u => u.ClientRepository.GetByIdAsync(clientId), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Null_When_Client_Does_Not_Exist()
        {
            // Arrange
            var clientId = 1;
            _mockUnitOfWork.Setup(u => u.ClientRepository.GetByIdAsync(clientId))
                .ReturnsAsync((DomainEntities.Client?)null);

            var query = new GetClientByIdQuery(clientId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
            _mockUnitOfWork.Verify(u => u.ClientRepository.GetByIdAsync(clientId), Times.Once);
        }
    }
}
