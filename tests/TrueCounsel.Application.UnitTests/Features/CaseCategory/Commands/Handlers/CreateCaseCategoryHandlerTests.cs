using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.CaseCategory.Commands;
using TrueCounsel.Application.Features.CaseCategory.Commands.Handlers;
using TrueCounsel.Application.Features.CaseCategory.Dtos;
using DomainEntities = TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseCategory.Commands.Handlers
{
    public class CreateCaseCategoryHandlerTests
    {
        private readonly Mock<ICaseCategory> _mockRepository;
        private readonly IMapper _mapper;
        private readonly CreateCaseCategoryHandler _handler;

        public CreateCaseCategoryHandlerTests()
        {
            _mockRepository = new Mock<ICaseCategory>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new CreateCaseCategoryHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Should_Create_CaseCategory_When_Command_Is_Valid()
        {
            // Arrange
            var command = new CreateCaseCategoryCommand
            {
                Name = "Property Dispute",
                Description = "Cases related to property",
                CaseTypeId = 1
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<DomainEntities.CaseCategory>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(command.Name);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<DomainEntities.CaseCategory>()), Times.Once);
        }
    }
}
