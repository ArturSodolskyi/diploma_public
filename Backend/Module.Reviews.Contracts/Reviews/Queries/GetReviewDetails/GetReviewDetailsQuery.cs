using MediatR;

namespace Module.Reviews.Contracts.Reviews.Queries.GetReviewDetails
{
    public class GetReviewDetailsQuery : IRequest<ReviewDetailsViewModel>
    {
        public int ReviewId { get; set; }
    }
}
