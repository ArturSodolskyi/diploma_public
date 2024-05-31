using MediatR;

namespace Module.Reviews.Contracts.Reviews.Queries.GetCompanyId
{
    public class GetCompanyIdQuery : IRequest<int>
    {
        public int ReviewId { get; set; }
    }
}
