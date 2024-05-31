using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Reviews.Contracts.Reviews.Commands.CompleteReview;
using Module.Reviews.Domain;
using Module.Reviews.Persistence;
using Shared.Exceptions;

namespace Module.Reviews.Application.Reviews.Commands.CompleteReview
{
    public class CompleteReviewCommandHandler : IRequestHandler<CompleteReviewCommand>
    {
        private readonly IReviewsDbContext _dbContext;
        public CompleteReviewCommandHandler(IReviewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CompleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _dbContext.Reviews
                .FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);
            if (review is null)
            {
                throw new NotFoundException(nameof(Review), request.ReviewId);
            }

            review.EndDate = DateTime.UtcNow;

            var reviewResult = new ReviewResult
            {
                Id = request.ReviewId,
                Comment = request.Comment
            };
            await _dbContext.ReviewResults.AddAsync(reviewResult, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
