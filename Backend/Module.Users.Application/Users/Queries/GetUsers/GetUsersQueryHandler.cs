using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Users.Contracts.Users.Queries.GetUsers;
using Module.Users.Persistence;
using Shared.Models;

namespace Module.Users.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserViewModel>>
    {
        private readonly IUsersDbContext _dbContext;
        public GetUsersQueryHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email!,
                    Role = (RoleEnum)_dbContext.UsersRoles.First(y => y.UserId == x.Id).RoleId
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
