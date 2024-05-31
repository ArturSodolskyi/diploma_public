using MediatR;
using Module.Companies.Contracts.Companies.Queries.GetUsers;
using Module.Companies.Contracts.UserCompanies.Queries.Exists;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Companies.Application.Companies.Queries.GetCompanyUsers
{
    public class GetCompanyUsersQueryAuthorizer : IAuthorizer<GetCompanyUsersQuery>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetCompanyUsersQueryAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(GetCompanyUsersQuery request, CancellationToken cancellationToken = default)
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
