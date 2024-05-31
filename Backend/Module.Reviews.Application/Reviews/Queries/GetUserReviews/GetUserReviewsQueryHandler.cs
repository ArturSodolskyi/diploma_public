using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Jobs.Queries.GetByIds;
using Module.Reviews.Contracts.Reviews.Queries.GetUserReviews;
using Module.Reviews.Domain;
using Module.Reviews.Persistence;

namespace Module.Reviews.Application.Reviews.Queries.GetUserReviews
{
    public class GetUserReviewsQueryHandler : IRequestHandler<GetUserReviewsQuery, List<UserReviewViewModel>>
    {
        private readonly IReviewsDbContext _dbContext;
        private readonly IMediator _mediator;
        public GetUserReviewsQueryHandler(IReviewsDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<List<UserReviewViewModel>> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _dbContext.Reviews
                .Include(x => x.ReviewTasks)
                .Where(x => x.RevieweeId == request.UserId && x.CompanyId == request.CompanyId)
                .ToListAsync(cancellationToken);
            var jobs = await _mediator.Send(new GetByIdsQuery
            {
                Ids = reviews.Select(x => x.JobId).ToList()
            }, cancellationToken);

            return reviews.Join(jobs, r => r.JobId, j => j.Id, (r, j) => new UserReviewViewModel
            {
                JobName = j.Name,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                Coverage = GetCoverage(r)
            }).ToList();
        }

        private static double? GetCoverage(Review review)
        {
            if (review.EndDate == null)
            {
                return null;
            }

            return review.ReviewTasks!
                .Select(y => y.Value)
                .Average();
        }
    }
}
