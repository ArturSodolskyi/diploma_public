using MediatR;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Module.Reviews.Contracts.Reviews.Queries.GetUserReviews;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Reviews.Application.Reviews.Queries.GetUserReviews
{
    public class GetUserReviewsQueryAuthorizer : IAuthorizer<GetUserReviewsQuery>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetUserReviewsQueryAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(GetUserReviewsQuery request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var isAdministrator = await _mediator.Send(new IsInCompanyRoleQuery
            {
                CompanyId = request.CompanyId,
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
