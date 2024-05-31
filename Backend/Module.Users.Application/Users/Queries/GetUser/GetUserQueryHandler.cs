using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Users.Contracts.Users.Queries.GetUser;
using Module.Users.Contracts.Users.Queries.GetUsers;
using Module.Users.Persistence;
using Shared.Models;

namespace Module.Users.Application.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
    {
        private readonly IUsersDbContext _dbContext;
        public GetUserQueryHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Where(x => x.Id == request.Id)
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email!,
                    Role = (RoleEnum)_dbContext.UsersRoles.First(y => y.UserId == x.Id).RoleId
                })
                .AsNoTracking()
                .SingleAsync(cancellationToken);
        }
    }
}
