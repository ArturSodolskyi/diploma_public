using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Reviews.Contracts.Reviews.Queries.GetCompanyId;
using Module.Reviews.Persistence;

namespace Module.Reviews.Application.Reviews.Queries.GetCompanyId
{
    public class GetCompanyIdQueryHandler : IRequestHandler<GetCompanyIdQuery, int>
    {
        private readonly IReviewsDbContext _dbContext;
        private readonly IMediator _mediator;
        public GetCompanyIdQueryHandler(IReviewsDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<int> Handle(GetCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _dbContext.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);

            if (review is null)
            {
                return 0;
            }

            return await _mediator.Send(new Explorer.Contracts.Jobs.Queries.GetCompanyId.GetCompanyIdQuery
            {
                JobId = review.JobId
            }, cancellationToken);
        }
    }
}
