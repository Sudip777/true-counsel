using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TrueCounsel.Application.Common.Mappings;
using TrueCounsel.Application.Features.CaseType.Commands;
using TrueCounsel.Application.Features.CaseType.Commands.Handlers;
using TrueCounsel.Application.Features.CaseType.Dtos;
using DomainEntities = TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseType.Commands.Handlers
{
    public class CreateCaseTypeHandlerTests
    {
        private readonly Mock<ICaseTypeRepository> _mockRepository;
        private readonly IMapper _mapper;
        private readonly CreateCaseTypeHandler _handler;

        public CreateCaseTypeHandlerTests()
        {
            _mockRepository = new Mock<ICaseTypeRepository>();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new CreateCaseTypeHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Should_Create_CaseType_When_Command_Is_Valid()
        {
            // Arrange
            var command = new CreateCaseTypeCommand
            {
                Name = "Civil",
                Description = "Civil Law Cases"
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<DomainEntities.CaseType>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(command.Name);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<DomainEntities.CaseType>()), Times.Once);
        }
    }
}
