using Microsoft.EntityFrameworkCore;
using Module.Reviews.Contracts.Reviews.Commands.CompleteReview;
using Module.Reviews.Domain;
using Module.Reviews.Persistence;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Reviews.Application.Reviews.Commands.CompleteReview
{
    public class CompleteReviewCommandAuthorizer : IAuthorizer<CompleteReviewCommand>
    {
        private readonly IReviewsDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        public CompleteReviewCommandAuthorizer(IReviewsDbContext dbContext,
            IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task AuthorizeAsync(CompleteReviewCommand request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var entity = await _dbContext.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Review), request.ReviewId);
            }

            if (entity.ReviewerId != _userAccessor.UserId)
            {
                throw new ForbiddenException();
            }
        }
    }
}