using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Reviews.Contracts.Reviews.Queries.GetReviewResult;
using Module.Reviews.Domain;
using Module.Reviews.Persistence;
using Shared.Exceptions;

namespace Module.Reviews.Application.Reviews.Queries.GetReviewResult
{
    public class GetReviewResultQueryHandler : IRequestHandler<GetReviewResultQuery, ReviewResultViewModel>
    {
        private readonly IReviewsDbContext _dbContext;
        public GetReviewResultQueryHandler(IReviewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ReviewResultViewModel> Handle(GetReviewResultQuery request, CancellationToken cancellationToken)
        {
            var reviewResult = await _dbContext.ReviewResults
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);
            if (reviewResult is null)
            {
                throw new NotFoundException(nameof(ReviewResult), request.ReviewId);
            }

            return new ReviewResultViewModel
            {
                Comment = reviewResult.Comment,
                Coverage = await GetCoverageAsync(request.ReviewId, cancellationToken)
            };
        }

        private async Task<double> GetCoverageAsync(int reviewId, CancellationToken cancellationToken)
        {
            var tasks = await _dbContext.ReviewTasks
                .Where(x => x.ReviewId == reviewId)
                .Select(x => x.Value)
                .ToListAsync(cancellationToken);
            if (!tasks.Any())
            {
                return 0;
            }

            return tasks.Average();
        }
    }
}
