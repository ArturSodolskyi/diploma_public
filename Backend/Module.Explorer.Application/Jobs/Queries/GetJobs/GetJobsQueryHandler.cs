using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Jobs.Queries.GetJobs;
using Module.Explorer.Persistence;

namespace Module.Explorer.Application.Jobs.Queries.GetJobs
{
    public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, List<JobViewModel>>
    {
        private readonly IExplorerDbContext _dbContext;
        public GetJobsQueryHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<JobViewModel>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Jobs
                .Where(x => x.CompanyId == request.CompanyId && x.Name.Contains(request.Filter))
                .Take(request.Amount)
                .Select(x => new JobViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
