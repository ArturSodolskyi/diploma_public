using MediatR;

namespace Module.Reviews.Contracts.Reviews.Queries.GetTasks
{
    public class GetTasksQuery : IRequest<List<TaskViewModel>>
    {
        public int ReviewId { get; set; }
        public int CompetenceId { get; set; }
    }
}
