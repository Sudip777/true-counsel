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
        public int CaseId { get;  set; }
        public int AuthorId { get;  set; }
        public CaseNoteType CaseNoteType { get;  set; } = CaseNoteType.General;
        public string Note { get;  set; } = String.Empty;
        public bool IsPrivate { get;  set; }

        // Navigation properties — these WILL be null until EF loads them
        public LegalCase? LegalCase { get;  set; }
        public User? Author { get; set; }
      
    }
}
