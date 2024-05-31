using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Competencies.Queries.GetCompanyId;
using Module.Explorer.Persistence;

namespace Module.Explorer.Application.Competencies.Queries.GetCompanyId
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
            var competence = await _dbContext.Competencies
                .Include(x => x.Job)
                .FirstOrDefaultAsync(x => x.Id == request.CompetenceId, cancellationToken);
            return competence is null ? 0 : competence.Job.CompanyId;
        }
    }
}
