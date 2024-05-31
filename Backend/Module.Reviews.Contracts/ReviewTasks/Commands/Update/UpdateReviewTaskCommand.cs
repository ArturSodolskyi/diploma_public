using MediatR;

namespace Module.Reviews.Contracts.ReviewTasks.Commands.Update
{
    public class UpdateReviewTaskCommand : IRequest
    {
        public int ReviewId { get; set; }
        public int TaskId { get; set; }
        public int Value { get; set; }
    }
}