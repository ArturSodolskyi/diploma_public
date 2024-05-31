using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Tasks.Queries.GetTasks;
using Module.Explorer.Persistence;

namespace Module.Explorer.Application.Tasks.Queries.GetTasks
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskViewModel>>
    {
        private readonly IExplorerDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetTasksQueryHandler(IExplorerDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<TaskViewModel>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Tasks
                .Where(x => x.CompetenceId == request.CompetenceId)
                .Select(x => _mapper.Map<TaskViewModel>(x))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}