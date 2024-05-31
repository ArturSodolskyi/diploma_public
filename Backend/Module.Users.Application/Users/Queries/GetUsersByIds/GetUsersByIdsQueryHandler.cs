using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Users.Contracts.Users.Queries.GetUsers;
using Module.Users.Contracts.Users.Queries.GetUsersByIds;
using Module.Users.Persistence;
using Shared.Models;

namespace Module.Users.Application.Users.Queries.GetUsersByIds
{
    public class GetUsersByIdsQueryHandler : IRequestHandler<GetUsersByIdsQuery, List<UserViewModel>>
    {
        private readonly IUsersDbContext _dbContext;
        public GetUsersByIdsQueryHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserViewModel>> Handle(GetUsersByIdsQuery request, CancellationToken cancellationToken)
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
                .Where(x => request.Ids.Contains(x.Id))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
