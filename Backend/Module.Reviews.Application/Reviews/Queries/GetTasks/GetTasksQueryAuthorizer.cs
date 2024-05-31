using Microsoft.EntityFrameworkCore;
using Module.Reviews.Contracts.Reviews.Queries.GetTasks;
using Module.Reviews.Domain;
using Module.Reviews.Persistence;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Reviews.Application.Reviews.Queries.GetTasks
{
    public class GetTasksQueryAuthorizer : IAuthorizer<GetTasksQuery>
    {
        private readonly IReviewsDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        public GetTasksQueryAuthorizer(IReviewsDbContext dbContext,
            IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task AuthorizeAsync(GetTasksQuery request, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Review), request.ReviewId);
            }

            if (entity.ReviewerId != _userAccessor.UserId && entity.RevieweeId != _userAccessor.UserId)
            {
                throw new ForbiddenException();
            }
        }
    }
}
