using MediatR;
using TrueCounsel.Application.Features.CaseNote.Dtos;

namespace TrueCounsel.Application.Features.CaseNote.Queries
{
    public class GetCaseNoteByIdQuery : IRequest<CaseNoteDto?>
    {
        public int Id { get; set; }

        public GetCaseNoteByIdQuery(int id)
        {
            Id = id;
        }
    }
}
