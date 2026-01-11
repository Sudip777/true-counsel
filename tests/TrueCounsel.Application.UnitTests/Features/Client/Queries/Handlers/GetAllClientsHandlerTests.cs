using System.Collections.Generic;
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
    public class GetAllClientsHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetAllClientsHandler _handler;

        public GetAllClientsHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetAllClientsHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_All_Clients()
        {
            // Arrange
            var clients = new List<DomainEntities.Client>
            {
                new DomainEntities.Client { Id = 1, Phone = "1234567890" },
                new DomainEntities.Client { Id = 2, Phone = "0987654321" }
            };

            _mockUnitOfWork.Setup(u => u.ClientRepository.GetAllAsync())
                .ReturnsAsync(clients);

            var query = new GetAllClientsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            _mockUnitOfWork.Verify(u => u.ClientRepository.GetAllAsync(), Times.Once);
        }
    }
}
