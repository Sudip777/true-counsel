using MediatR;
using System.Collections.Generic;
using TrueCounsel.Application.Features.CaseNote.Dtos;

namespace TrueCounsel.Application.Features.CaseNote.Queries
{
    public class GetAllCaseNoteQuery : IRequest<IEnumerable<CaseNoteDto>>
    {
    }
}
