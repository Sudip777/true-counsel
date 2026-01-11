using System;
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
    public class CasePartyQueryHandlersTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public CasePartyQueryHandlersTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetAllCasePartiesHandler_Should_Return_All_Parties()
        {
            // Arrange
            var parties = new List<DomainEntities.CaseParty>
            {
                new DomainEntities.CaseParty { Id = 1, Name = "P1" },
                new DomainEntities.CaseParty { Id = 2, Name = "P2" }
            };

            _mockUnitOfWork.Setup(u => u.CasePartyRepository.GetAllAsync())
                .ReturnsAsync(parties);

            var handler = new GetAllCasePartiesHandler(_mockUnitOfWork.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetAllCasePartiesQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetCasePartyByIdHandler_Should_Return_Party_When_Exists()
        {
            // Arrange
            var party = new DomainEntities.CaseParty { Id = 1, Name = "P1" };

            _mockUnitOfWork.Setup(u => u.CasePartyRepository.GetByIdAsync(1))
                .ReturnsAsync(party);

            var handler = new GetCasePartyByIdHandler(_mockUnitOfWork.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetCasePartyByIdQuery(1), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }
    }
}
