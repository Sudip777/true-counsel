using System;
using TrueCounsel.Domain.Enums;

namespace TrueCounsel.Application.Features.CaseNote.Dtos
{
    public class CaseNoteDto
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public int AuthorId { get; set; }
        public CaseNoteType CaseNoteType { get; set; }
        public required string Note { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
