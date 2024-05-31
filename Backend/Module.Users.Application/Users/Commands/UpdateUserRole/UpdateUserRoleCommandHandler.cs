using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.Users.Contracts.Users.Commands.UpdateUserRole;
using Module.Users.Domain;
using Module.Users.Persistence;
using Shared.Exceptions;

namespace Module.Users.Application.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand>
    {
        private readonly IUsersDbContext _dbContext;
        public UpdateUserRoleCommandHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.UsersRoles
                .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(UserRole), request.UserId);
            }

            _dbContext.UsersRoles.Remove(entity);

            var addModel = new IdentityUserRole<int>
            {
                UserId = request.UserId,
                RoleId = (int)request.Role
            };

            await _dbContext.UsersRoles.AddAsync(addModel, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
