using MediatR;

namespace Module.Explorer.Contracts.Tasks.Queries.GetTaskIdsByJob
{
    public class GetTaskIdsByJobQuery : IRequest<List<int>>
    {
        public int JobId { get; set; }
    }
}
