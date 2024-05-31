using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Jobs.Queries.GetByIds;
using Module.Explorer.Contracts.Jobs.Queries.GetJobs;
using Module.Explorer.Persistence;

namespace Module.Explorer.Application.Jobs.Queries.GetByIds
{
    public class GetByIdsQueryHandler : IRequestHandler<GetByIdsQuery, List<JobViewModel>>
    {
        private readonly IExplorerDbContext _dbContext;
        public GetByIdsQueryHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<JobViewModel>> Handle(GetByIdsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Jobs
                .Where(x => request.Ids.Contains(x.Id))
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
