using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Users.Contracts.Users.Commands.DeleteUser;
using Module.Users.Domain;
using Module.Users.Persistence;
using Shared.Exceptions;

namespace Module.Users.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUsersDbContext _dbContext;
        public DeleteUserCommandHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            _dbContext.Users.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
