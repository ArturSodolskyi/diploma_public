using Module.Users.Contracts.Users.Commands.UpdateUserRole;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Users.Application.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandAuthorizer : IAuthorizer<UpdateUserRoleCommand>
    {
        private readonly IUserAccessor _userAccessor;
        public UpdateUserRoleCommandAuthorizer(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public Task AuthorizeAsync(UpdateUserRoleCommand request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return Task.CompletedTask;
            }

            throw new ForbiddenException();
        }
    }
}