using MediatR;

namespace Module.Reviews.Contracts.Reviews.Queries.GetReviews
{
    public class GetReviewsQuery : IRequest<List<ReviewViewModel>>
    {

    }
}

