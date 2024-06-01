using MediatR;

namespace Module.Reviews.Contracts.ReviewTasks.Commands.DeleteByTask
{
    public class DeleteByTaskCommand : IRequest
    {
        public int TaskId { get; set; }
    }
}