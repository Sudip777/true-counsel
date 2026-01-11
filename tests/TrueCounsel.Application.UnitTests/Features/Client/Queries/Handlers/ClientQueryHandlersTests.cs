using System;
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
    public class ClientQueryHandlersTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public ClientQueryHandlersTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetAllClientsHandler_Should_Return_All_Clients()
        {
            // Arrange
            var clients = new List<DomainEntities.Client>
            {
                new DomainEntities.Client { Id = 1, Phone = "123" },
                new DomainEntities.Client { Id = 2, Phone = "456" }
            };

            _mockUnitOfWork.Setup(u => u.ClientRepository.GetAllAsync())
                .ReturnsAsync(clients);

            var handler = new GetAllClientsHandler(_mockUnitOfWork.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetAllClientsQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetClientByIdHandler_Should_Return_Client_When_Exists()
        {
            // Arrange
            var client = new DomainEntities.Client { Id = 1, Phone = "123" };

            _mockUnitOfWork.Setup(u => u.ClientRepository.GetByIdAsync(1))
                .ReturnsAsync(client);

            var handler = new GetClientByIdHandler(_mockUnitOfWork.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetClientByIdQuery(1), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }
    }
}
