using MediatR;
using TrueCounsel.Application.Features.Lawyer.Dtos;

namespace TrueCounsel.Application.Features.Lawyer.Queries
{
    public class GetLawyerByIdQuery : IRequest<LawyerDto?>
    {
        public int Id { get; set; }

        public GetLawyerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
