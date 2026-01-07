using System;
using FluentAssertions;
using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Enums;
using TrueCounsel.Domain.Exceptions;
using TrueCounsel.Domain.Services;
using Xunit;

namespace TrueCounsel.Domain.UnitTests.Services
{
    public class LegalCaseServiceTests
    {
        private readonly LegalCaseService _service;

        public LegalCaseServiceTests()
        {
            _service = new LegalCaseService();
        }

        [Theory]
        [InlineData(CaseStatus.Ongoing, true)]
        [InlineData(CaseStatus.PendingReview, true)]
        [InlineData(CaseStatus.New, false)]
        [InlineData(CaseStatus.Closed, false)]
        [InlineData(CaseStatus.Archived, false)]
        public void CanCloseCase_ShouldReturnExpectedResult(CaseStatus status, bool expected)
        {
            // Arrange
            var legalCase = new LegalCase
            {
                CaseNumber = "TEST-001",
                Status = status
            };

            // Act
            var result = _service.CanCloseCase(legalCase);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void CanCloseCase_ShouldThrowArgumentNullException_WhenCaseIsNull()
        {
            // Act
            Action act = () => _service.CanCloseCase(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CloseCase_ShouldUpdateStatusAndClosingDate_WhenCaseIsOngoing()
        {
            // Arrange
            var legalCase = new LegalCase
            {
                CaseNumber = "TEST-001",
                Status = CaseStatus.Ongoing
            };
            var outcome = CaseOutcome.Won;
            var notes = "Case won in court.";

            // Act
            _service.CloseCase(legalCase, outcome, notes);

            // Assert
            legalCase.Status.Should().Be(CaseStatus.Closed);
            legalCase.Outcome.Should().Be(outcome);
            legalCase.OutcomeNotes.Should().Be(notes);
            legalCase.ClosingDate.Should().NotBeNull();
            legalCase.ClosingDate.Value.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void CloseCase_ShouldThrowDomainException_WhenCaseCannotBeClosed()
        {
            // Arrange
            var legalCase = new LegalCase
            {
                CaseNumber = "TEST-001",
                Status = CaseStatus.New
            };

            // Act
            Action act = () => _service.CloseCase(legalCase, CaseOutcome.Won, "Notes");

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("*Cannot close case*");
        }

        [Fact]
        public void CloseCase_ShouldThrowArgumentNullException_WhenCaseIsNull()
        {
            // Act
            Action act = () => _service.CloseCase(null!, CaseOutcome.Won, "Notes");

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CloseCase_ShouldUpdateStatusAndClosingDate_WhenCaseIsPendingReview()
        {
            // Arrange
            var legalCase = new LegalCase
            {
                CaseNumber = "TEST-002",
                Status = CaseStatus.PendingReview
            };
            var outcome = CaseOutcome.Settled;
            var notes = "Settled out of court.";

            // Act
            _service.CloseCase(legalCase, outcome, notes);

            // Assert
            legalCase.Status.Should().Be(CaseStatus.Closed);
            legalCase.Outcome.Should().Be(outcome);
            legalCase.OutcomeNotes.Should().Be(notes);
            legalCase.ClosingDate.Should().NotBeNull();
            legalCase.ClosingDate.Value.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }
    }
}
