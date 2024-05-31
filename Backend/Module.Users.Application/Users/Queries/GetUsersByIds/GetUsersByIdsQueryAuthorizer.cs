using MediatR;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Queries.Exists;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Module.Users.Contracts.Users.Queries.GetUsersByIds;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Users.Application.Users.Queries.GetUsersByIds
{
    public class GetUsersByIdsQueryAuthorizer : IAuthorizer<GetUsersByIdsQuery>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetUsersByIdsQueryAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(GetUsersByIdsQuery request, CancellationToken cancellationToken = default)
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

            foreach (var userId in request.Ids)
            {
                var exists = await _mediator.Send(new ExistsQuery
                {
                    CompanyId = request.CompanyId,
                    UserId = userId,
                }, cancellationToken);

                if (!exists)
                {
                    throw new ForbiddenException();
                }
            }
        }
    }
}
