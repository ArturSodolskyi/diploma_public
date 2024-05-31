using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Jobs.Queries.GetJob;
using Module.Explorer.Contracts.Jobs.Queries.GetJobs;
using Module.Explorer.Persistence;

namespace Module.Explorer.Application.Jobs.Queries.GetJob
{
    public class GetJobQueryHandler : IRequestHandler<GetJobQuery, JobViewModel>
    {
        private readonly IExplorerDbContext _dbContext;
        public GetJobQueryHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<JobViewModel> Handle(GetJobQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Jobs
                .Where(x => x.Id == request.Id)
                .Select(x => new JobViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .AsNoTracking()
                .SingleAsync(cancellationToken);
        }
    }
}
