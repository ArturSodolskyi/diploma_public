using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Jobs.Queries.GetCompanyId;
using Module.Explorer.Persistence;

namespace Module.Explorer.Application.Jobs.Queries.GetCompanyId
{
    public class GetCompanyIdQueryHandler : IRequestHandler<GetCompanyIdQuery, int>
    {
        private readonly IExplorerDbContext _dbContext;
        public GetCompanyIdQueryHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(GetCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _dbContext.Jobs.FirstOrDefaultAsync(x => x.Id == request.JobId, cancellationToken);
            return job is null ? 0 : job.CompanyId;
        }
    }
}
