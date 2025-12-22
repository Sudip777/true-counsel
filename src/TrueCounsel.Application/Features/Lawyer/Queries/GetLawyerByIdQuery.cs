namespace TrueCounsel.Application.Features.Lawyer.Queries
{
    public class GetLawyerByIdQuery
    {
        public int Id { get; set; }

        public GetLawyerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
