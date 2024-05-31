using Module.Users.Contracts.Users.Commands.DeleteUser;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Users.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandAuthorizer : IAuthorizer<DeleteUserCommand>
    {
        private readonly IUserAccessor _userAccessor;
        public DeleteUserCommandAuthorizer(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public Task AuthorizeAsync(DeleteUserCommand request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return Task.CompletedTask;
            }

            throw new ForbiddenException();
        }
    }
}