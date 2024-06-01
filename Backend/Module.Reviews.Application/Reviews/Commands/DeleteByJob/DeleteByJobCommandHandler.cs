using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Reviews.Contracts.Reviews.Commands.DeleteByJob;
using Module.Reviews.Persistence;

namespace Module.Reviews.Application.Reviews.Commands.DeleteByJob
{
    public class DeleteByJobCommandHandler : IRequestHandler<DeleteByJobCommand>
    {
        private readonly IReviewsDbContext _dbContext;
        public DeleteByJobCommandHandler(IReviewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteByJobCommand request, CancellationToken cancellationToken)
        {
            var reviews = await _dbContext.Reviews.Where(x => x.JobId == request.JobId)
                .ToListAsync(cancellationToken);
            _dbContext.Reviews.RemoveRange(reviews);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}