using MediatR;

namespace Module.Reviews.Contracts.Reviews.Queries.GetUserReviews
{
    public class GetUserReviewsQuery : IRequest<List<UserReviewViewModel>>
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
    }
}
