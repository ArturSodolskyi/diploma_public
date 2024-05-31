using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Reviews.Contracts.ReviewTasks.Commands.Update;
using Module.Reviews.Domain;
using Module.Reviews.Persistence;
using Shared.Exceptions;

namespace Module.Reviews.Application.ReviewTasks.Commands.Update
{
    public class UpdateReviewTaskCommandHandler : IRequestHandler<UpdateReviewTaskCommand>
    {
        private readonly IReviewsDbContext _dbContext;
        public UpdateReviewTaskCommandHandler(IReviewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateReviewTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.ReviewTasks
                .FirstOrDefaultAsync(x => x.ReviewId == request.ReviewId && x.TaskId == request.TaskId, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(ReviewTask), $"{request.ReviewId}-{request.TaskId}");
            }

            entity.Value = request.Value;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
