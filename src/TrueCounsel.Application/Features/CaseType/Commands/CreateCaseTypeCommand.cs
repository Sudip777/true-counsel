using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueCounsel.Application.Features.CaseType.Commands
{
    public class CreateCaseTypeCommand
    {

        public required string Name { get; set; }
        public string Description { get; set; } = String.Empty;

    }
}
