using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Jobs.Queries.GetJob;
using Module.Reviews.Contracts.Reviews.Queries.GetReviewDetails;
using Module.Reviews.Domain;
using Module.Reviews.Persistence;
using Module.Users.Contracts.Users.Queries.GetUser;
using Shared.Exceptions;

namespace Module.Reviews.Application.Reviews.Queries.GetReviewDetails
{
    public class GetReviewDetailsQueryHandler : IRequestHandler<GetReviewDetailsQuery, ReviewDetailsViewModel>
    {
        private readonly IReviewsDbContext _dbContext;
        private readonly IMediator _mediator;
        public GetReviewDetailsQueryHandler(IReviewsDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<ReviewDetailsViewModel> Handle(GetReviewDetailsQuery request, CancellationToken cancellationToken)
        {
            var review = await _dbContext.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);
            if (review is null)
            {
                throw new NotFoundException(nameof(Review), request.ReviewId);
            }

            var job = await _mediator.Send(new GetJobQuery { Id = review.JobId }, cancellationToken);
            var reviewee = await _mediator.Send(new GetUserQuery
            {
                Id = review.RevieweeId,
                CompanyId = review.CompanyId
            }, cancellationToken);
            var reviewer = await _mediator.Send(new GetUserQuery
            {
                Id = review.ReviewerId,
                CompanyId = review.CompanyId
            }, cancellationToken);
            var requestor = await _mediator.Send(new GetUserQuery
            {
                Id = review.RequestorId,
                CompanyId = review.CompanyId
            }, cancellationToken);

            return new ReviewDetailsViewModel
            {
                Job = job.Name,
                Reviewee = $"{reviewee.FirstName} {reviewee.LastName}",
                Reviewer = $"{reviewer.FirstName} {reviewer.LastName}",
                Requestor = $"{requestor.FirstName} {requestor.LastName}",
            };
        }
    }
}
