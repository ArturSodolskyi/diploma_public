using MediatR;

namespace Module.Reviews.Contracts.Reviews.Commands.DeleteByJob
{
    public class DeleteByJobCommand : IRequest
    {
        public int JobId { get; set; }
    }
}
