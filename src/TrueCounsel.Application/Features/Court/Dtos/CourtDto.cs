using System;

namespace TrueCounsel.Application.Features.Court.Dtos
{
    /// <summary>
    /// Data Transfer Object for Court entity.
    /// Used to expose court information to API consumers.
    /// </summary>
    public class CourtDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int CourtTypeId { get; set; }
        public string CourtTypeName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
