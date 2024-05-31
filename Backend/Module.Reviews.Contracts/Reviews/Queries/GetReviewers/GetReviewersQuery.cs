using MediatR;

namespace Module.Reviews.Contracts.Reviews.Queries.GetReviewers
{
    public class GetReviewersQuery : IRequest<List<ReviewerViewModel>>
    {
        public int CompanyId { get; set; }
        public required string Filter { get; set; }
        public int Amount { get; set; }
    }
}
