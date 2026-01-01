using MediatR;
using TrueCounsel.Application.Features.Lawyer.Dtos;

namespace TrueCounsel.Application.Features.Lawyer.Commands
{
    public class UpdateLawyerCommand : IRequest<LawyerDto>
    {
        public int Id { get; set; }
        public string? Specialization { get; set; }
        public int? YearsOfExperience { get; set; }
        public decimal HourlyRate { get; set; }
        public string? Bio { get; set; }
    }
}
