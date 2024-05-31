using MediatR;

namespace Module.Explorer.Contracts.Tasks.Queries.GetTasks
{
    public class GetTasksQuery : IRequest<List<TaskViewModel>>
    {
        public int CompetenceId { get; set; }
    }
}
