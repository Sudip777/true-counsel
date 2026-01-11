using System;
using System.Collections.Generic;
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
    public class CourtQueryHandlersTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public CourtQueryHandlersTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetAllCourtsHandler_Should_Return_All_Courts()
        {
            // Arrange
            var courts = new List<DomainEntities.Court>
            {
                new DomainEntities.Court { Id = 1, Name = "C1" },
                new DomainEntities.Court { Id = 2, Name = "C2" }
            };

            _mockUnitOfWork.Setup(u => u.CourtRepository.GetAllAsync())
                .ReturnsAsync(courts);

            var handler = new GetAllCourtsHandler(_mockUnitOfWork.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetAllCourtsQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetCourtByIdHandler_Should_Return_Court_When_Exists()
        {
            // Arrange
            var court = new DomainEntities.Court { Id = 1, Name = "C1" };

            _mockUnitOfWork.Setup(u => u.CourtRepository.GetByIdAsync(1))
                .ReturnsAsync(court);

            var handler = new GetCourtByIdHandler(_mockUnitOfWork.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetCourtByIdQuery(1), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }
    }
}
