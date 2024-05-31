using MediatR;

namespace Module.Reviews.Contracts.Reviews.Queries.GetReviewResult
{
    public class GetReviewResultQuery : IRequest<ReviewResultViewModel>
    {
        public int ReviewId { get; set; }
    }
}