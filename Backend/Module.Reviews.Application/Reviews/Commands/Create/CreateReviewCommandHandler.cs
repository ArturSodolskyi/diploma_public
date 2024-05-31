using MediatR;
using Module.Explorer.Contracts.Jobs.Queries.GetCompanyId;
using Module.Explorer.Contracts.Tasks.Queries.GetTaskIdsByJob;
using Module.Reviews.Contracts.Reviews.Commands.Create;
using Module.Reviews.Domain;
using Module.Reviews.Persistence;
using Shared.Accessors;
using System.Transactions;

namespace Module.Reviews.Application.Reviews.Commands.Create
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
    {
        private readonly IReviewsDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public CreateReviewCommandHandler(IReviewsDbContext dbContext,
            IUserAccessor userAccessor,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var companyId = await _mediator.Send(new GetCompanyIdQuery
            {
                JobId = request.JobId
            }, cancellationToken);

            var item = new Review
            {
                JobId = request.JobId,
                RevieweeId = request.RevieweeId,
                RequestorId = _userAccessor.UserId,
                ReviewerId = request.ReviewerId,
                StartDate = DateTime.UtcNow,
                CompanyId = companyId
            };

            using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

            await _dbContext.Reviews.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var tasks = await _mediator.Send(new GetTaskIdsByJobQuery
            {
                JobId = request.JobId
            }, cancellationToken);

            var reviewTasks = tasks.Select(x => new ReviewTask
            {
                ReviewId = item.Id,
                TaskId = x
            });

            await _dbContext.ReviewTasks.AddRangeAsync(reviewTasks, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            scope.Complete();

            return item.Id;
        }
    }
}
