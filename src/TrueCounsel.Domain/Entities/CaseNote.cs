using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;
using TrueCounsel.Domain.Enums;

namespace TrueCounsel.Domain.Entities
{
    public class CaseNote : BaseAuditableEntity
    {
        public int CaseId { get; private set; }
        public int AuthorId { get; private set; }
        public CaseNoteType CaseNoteType { get; private set; } = CaseNoteType.General;
        public string Note { get; private set; } = String.Empty;
        public bool IsPrivate { get; private set; }

        // Navigation properties — these WILL be null until EF loads them
        public Case? Case { get; private set; }
        public User? Author { get; private set; }
        private CaseNote() { }

        public static CaseNote Create(Case @case, User author, string note, CaseNoteType type = CaseNoteType.General, bool isPrivate = false)
        {
            ArgumentNullException.ThrowIfNull(@case);
            ArgumentNullException.ThrowIfNull(@author);
            ArgumentNullException.ThrowIfNull(@note);

            return new CaseNote
            {
                CaseId = @case.Id,
                Case = @case,
                AuthorId = author.Id,
                Author = author,
                Note = note.Trim(),
                CaseNoteType = type,
                IsPrivate = isPrivate,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
