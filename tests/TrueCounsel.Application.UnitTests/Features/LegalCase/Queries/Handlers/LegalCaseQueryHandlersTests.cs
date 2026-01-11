using System;
using System.Collections.Generic;
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
    public class LegalCaseQueryHandlersTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public LegalCaseQueryHandlersTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetAllLegalCasesHandler_Should_Return_All_Cases()
        {
            // Arrange
            var cases = new List<DomainEntities.LegalCase>
            {
                new DomainEntities.LegalCase { Id = 1, Title = "C1", CaseNumber = "CN1", ClientId = 1, LawyerId = 1, CaseTypeId = 1 },
                new DomainEntities.LegalCase { Id = 2, Title = "C2", CaseNumber = "CN2", ClientId = 1, LawyerId = 1, CaseTypeId = 1 }
            };

            _mockUnitOfWork.Setup(u => u.LegalCaseRepository.GetAllAsync())
                .ReturnsAsync(cases);

            var handler = new GetAllLegalCasesHandler(_mockUnitOfWork.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetAllLegalCasesQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetLegalCaseByIdHandler_Should_Return_Case_When_Exists()
        {
            // Arrange
            var legalCase = new DomainEntities.LegalCase { Id = 1, Title = "C1", CaseNumber = "CN1", ClientId = 1, LawyerId = 1, CaseTypeId = 1 };

            _mockUnitOfWork.Setup(u => u.LegalCaseRepository.GetByIdAsync(1))
                .ReturnsAsync(legalCase);

            var handler = new GetLegalCaseByIdHandler(_mockUnitOfWork.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetLegalCaseByIdQuery(1), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }
    }
}
