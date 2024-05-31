using MediatR;
using Module.Companies.Contracts.UserCompanies.Queries.Exists;
using Module.Reviews.Contracts.Reviews.Queries.GetReviewers;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Reviews.Application.Reviews.Queries.GetReviewers
{
    public class GetReviewersQueryAuthorizer : IAuthorizer<GetReviewersQuery>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetReviewersQueryAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(GetReviewersQuery request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var exists = await _mediator.Send(new ExistsQuery
            {
                UserId = _userAccessor.UserId,
                CompanyId = request.CompanyId
            }, cancellationToken);

            if (!exists)
            {
                throw new ForbiddenException();
            }
        }
    }
}
