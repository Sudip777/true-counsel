using System.Collections.Generic;
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
    public class GetAllLawyersHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly GetAllLawyersHandler _handler;

        public GetAllLawyersHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new GetAllLawyersHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task Should_Return_All_Lawyers()
        {
            // Arrange
            var lawyers = new List<DomainEntities.Lawyer>
            {
                new DomainEntities.Lawyer { Id = 1, BarNumber = "L123", HourlyRate = 100 },
                new DomainEntities.Lawyer { Id = 2, BarNumber = "L456", HourlyRate = 120 }
            };

            _mockUnitOfWork.Setup(u => u.LawyerRepository.GetAllAsync())
                .ReturnsAsync(lawyers);

            var query = new GetAllLawyersQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            _mockUnitOfWork.Verify(u => u.LawyerRepository.GetAllAsync(), Times.Once);
        }
    }
}
