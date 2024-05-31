using MediatR;
using Module.Explorer.Contracts.Jobs.Queries.GetJobs;

namespace Module.Explorer.Contracts.Jobs.Queries.GetJob
{
    public class GetJobQuery : IRequest<JobViewModel>
    {
        public int Id { get; set; }
    }
}
