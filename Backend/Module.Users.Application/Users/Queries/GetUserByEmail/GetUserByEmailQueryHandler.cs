using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Users.Contracts.Users.Queries.GetUserByEmail;
using Module.Users.Contracts.Users.Queries.GetUsers;
using Module.Users.Persistence;
using Shared.Models;

namespace Module.Users.Application.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserViewModel?>
    {
        private readonly IUsersDbContext _dbContext;
        public GetUserByEmailQueryHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserViewModel?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName!,
                    LastName = x.LastName!,
                    Email = x.Email!,
                    Role = (RoleEnum)_dbContext.UsersRoles.First(y => y.UserId == x.Id).RoleId
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        }
    }
}
