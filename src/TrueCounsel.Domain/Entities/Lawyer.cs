using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;
using TrueCounsel.Domain.ValueObjects;

namespace TrueCounsel.Domain.Entities
{
    public class Lawyer : BaseAuditableEntity
    {
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;
        public string BarNumber { get; private set; } = null!;
        public string? Specialization { get; private set; }
        public int? YearsOfExperience { get; private set; }
        public Money HourlyRate { get; private set; } = 1;        // ← VO
        public string? Bio { get; private set; }

        public IReadOnlyCollection<Case> AssignedCases => _assignedCases.AsReadOnly();
        private readonly List<Case> _assignedCases = new();

        public static Lawyer Create(User user, string barNumber, Money hourlyRate)
        {
            ArgumentNullException.ThrowIfNull(user);

            return new Lawyer
            {
                UserId = user.Id,
                User = user,
                BarNumber = barNumber,
                HourlyRate = hourlyRate,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void UpdateHourlyRate(Money newRate)
        {
            ArgumentNullException.ThrowIfNull(newRate);

            if (newRate.Amount < 0) throw new Domain.Exceptions.NegativeAmountNotAllowedException();
            HourlyRate = newRate;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
