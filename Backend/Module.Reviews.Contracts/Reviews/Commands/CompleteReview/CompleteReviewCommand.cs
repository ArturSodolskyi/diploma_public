using MediatR;

namespace Module.Reviews.Contracts.Reviews.Commands.CompleteReview
{
    public class CompleteReviewCommand : IRequest
    {
        public int ReviewId { get; set; }
        public required string Comment { get; set; }
    }
}
