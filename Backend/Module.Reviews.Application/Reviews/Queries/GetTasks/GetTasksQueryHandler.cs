using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Reviews.Contracts.Reviews.Queries.GetTasks;
using Module.Reviews.Persistence;

namespace Module.Reviews.Application.Reviews.Queries.GetTasks
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskViewModel>>
    {
        private readonly IReviewsDbContext _dbContext;
        private readonly IMediator _mediator;
        public GetTasksQueryHandler(IReviewsDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<List<TaskViewModel>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _mediator.Send(new Explorer.Contracts.Tasks.Queries.GetTasks.GetTasksQuery
            {
                CompetenceId = request.CompetenceId,
            }, cancellationToken);

            var reviewTasks = await _dbContext.ReviewTasks
                .Where(x => x.ReviewId == request.ReviewId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return reviewTasks
                .Join(tasks, rt => rt.TaskId, t => t.Id, (rt, t) => new TaskViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Text = t.Text,
                    Value = rt.Value
                })
                .ToList();
        }
    }
}