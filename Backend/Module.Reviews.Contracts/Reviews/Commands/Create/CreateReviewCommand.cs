using MediatR;

namespace Module.Reviews.Contracts.Reviews.Commands.Create
{
    public class CreateReviewCommand : IRequest<int>
    {
        public int JobId { get; set; }
        public int RevieweeId { get; set; }
        public int ReviewerId { get; set; }
    }
}