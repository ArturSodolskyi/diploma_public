using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Tasks.Queries.GetTaskIdsByJob;
using Module.Explorer.Persistence;

namespace Module.Explorer.Application.Tasks.Queries.GetTaskIdsByJob
{
    public class GetTaskIdsByJobQueryHandler : IRequestHandler<GetTaskIdsByJobQuery, List<int>>
    {
        private readonly IExplorerDbContext _dbContext;
        public GetTaskIdsByJobQueryHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<int>> Handle(GetTaskIdsByJobQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Tasks
                .Include(x => x.Competence)
                .Include(x => x.Competence!.Job)
                .Where(x => x.Competence!.Job!.Id == request.JobId)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
