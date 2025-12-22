namespace TrueCounsel.Application.Features.Lawyer.Commands
{
    public class UpdateLawyerCommand
    {
        public int Id { get; set; }
        public string? Specialization { get; set; }
        public int? YearsOfExperience { get; set; }
        public decimal HourlyRate { get; set; }
        public string? Bio { get; set; }
    }
}
