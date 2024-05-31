using MediatR;

namespace Module.Explorer.Contracts.Jobs.Queries.GetJobs
{
    public class GetJobsQuery : IRequest<List<JobViewModel>>
    {
        public int CompanyId { get; set; }
        public required string Filter { get; set; }
        public int Amount { get; set; }
    }
}
