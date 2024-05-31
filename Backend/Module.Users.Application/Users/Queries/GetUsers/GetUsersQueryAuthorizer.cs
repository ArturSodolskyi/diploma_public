using Module.Users.Contracts.Users.Queries.GetUsers;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Users.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryAuthorizer : IAuthorizer<GetUsersQuery>
    {
        private readonly IUserAccessor _userAccessor;
        public GetUsersQueryAuthorizer(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public Task AuthorizeAsync(GetUsersQuery request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return Task.CompletedTask;
            }

            throw new ForbiddenException();
        }
    }
}
