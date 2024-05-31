using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Module.Reviews.Contracts.Reviews.Queries.GetCompanyId;
using Module.Reviews.Domain;
using Module.Reviews.Persistence;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Reviews.Application.Reviews.Queries.GetCompanyId
{
    public class GetCompanyIdQueryAuthorizer : IAuthorizer<GetCompanyIdQuery>
    {
        private readonly IReviewsDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetCompanyIdQueryAuthorizer(IReviewsDbContext dbContext,
            IUserAccessor userAccessor,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(GetCompanyIdQuery request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var review = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);
            if (review is null)
            {
                throw new NotFoundException(nameof(Review), request.ReviewId);
            }

            var companyId = review.CompanyId;
            var isAdministrator = await _mediator.Send(new IsInCompanyRoleQuery
            {
                CompanyId = companyId,
                UserId = _userAccessor.UserId,
                Role = CompanyRoleEnum.Administrator
            }, cancellationToken);

            if (!isAdministrator)
            {
                throw new ForbiddenException();
            }
        }
    }
}
