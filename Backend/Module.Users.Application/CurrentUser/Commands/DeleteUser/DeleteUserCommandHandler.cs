using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Users.Contracts.CurrentUser.Commands.DeleteUser;
using Module.Users.Domain;
using Module.Users.Persistence;
using Shared.Accessors;
using Shared.Exceptions;

namespace Module.Users.Application.CurrentUser.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        public DeleteUserCommandHandler(IUsersDbContext dbContext,
            IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == _userAccessor.UserId, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(User), _userAccessor.UserId);
            }

            _dbContext.Users.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
