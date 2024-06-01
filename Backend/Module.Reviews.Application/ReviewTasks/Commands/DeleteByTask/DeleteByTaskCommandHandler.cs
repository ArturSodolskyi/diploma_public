using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Reviews.Contracts.ReviewTasks.Commands.DeleteByTask;
using Module.Reviews.Persistence;

namespace Module.Reviews.Application.ReviewTasks.Commands.DeleteByTask
{
    public class DeleteByTaskCommandHandler : IRequestHandler<DeleteByTaskCommand>
    {
        private readonly IReviewsDbContext _dbContext;
        public DeleteByTaskCommandHandler(IReviewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteByTaskCommand request, CancellationToken cancellationToken)
        {
            var reviewTasks = await _dbContext.ReviewTasks
                .Where(x => x.TaskId == request.TaskId)
                .ToListAsync(cancellationToken);
            _dbContext.ReviewTasks.RemoveRange(reviewTasks);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
