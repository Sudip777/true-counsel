namespace TrueCounsel.Application.Features.Lawyer.Dtos
{
    /// <summary> data transfer objct for lawer </summary>
    public class LawyerDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string BarNumber { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public int? YearsOfExperience { get; set; }
        public decimal HourlyRate { get; set; }
        public string? Bio { get; set; }
    }
}
