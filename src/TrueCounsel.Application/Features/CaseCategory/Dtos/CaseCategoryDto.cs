using System;

namespace TrueCounsel.Application.Features.CaseCategory.Dtos
{
    public class CaseCategoryDto
    {
        public int Id { get; set; }
        public int CaseTypeId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
