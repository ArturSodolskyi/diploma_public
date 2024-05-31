using MediatR;
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
            var job = _dbContext.Jobs.FirstOrDefault(x => x.Id == request.JobId);
            return job is null ? 0 : job.CompanyId;
        }
    }
}
