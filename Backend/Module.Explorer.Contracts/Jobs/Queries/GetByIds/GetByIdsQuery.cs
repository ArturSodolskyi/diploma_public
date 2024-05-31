using MediatR;
using Module.Explorer.Contracts.Jobs.Queries.GetJobs;

namespace Module.Explorer.Contracts.Jobs.Queries.GetByIds
{
    public class GetByIdsQuery : IRequest<List<JobViewModel>>
    {
        public required IEnumerable<int> Ids { get; set; }
    }
}
