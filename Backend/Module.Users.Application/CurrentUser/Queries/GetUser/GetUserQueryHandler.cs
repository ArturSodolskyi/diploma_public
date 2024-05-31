using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Queries.GetUserCompanyRole;
using Module.Users.Contracts.CurrentUser.Queries.GetUser;
using Module.Users.Persistence;
using Shared.Accessors;
using Shared.Models;

namespace Module.Users.Application.CurrentUser.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetUserQueryHandler(IUsersDbContext dbContext,
            IUserAccessor userAccessor,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .SingleAsync(x => x.Id == _userAccessor.UserId, cancellationToken);

            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                Role = await GetRoleAsync(user.Id),
                CompanyId = user.CompanyId,
                CompanyRole = await GetCompanyRoleAsync(user.Id, user.CompanyId)
            };
        }

        private async Task<RoleEnum> GetRoleAsync(int userId)
        {
            return (RoleEnum)(await _dbContext.UsersRoles.FirstAsync(y => y.UserId == userId)).RoleId;
        }

        private async Task<CompanyRoleEnum?> GetCompanyRoleAsync(int userId, int? companyId)
        {
            if (companyId is not null)
            {
                return await _mediator.Send(new GetUserCompanyRoleQuery
                {
                    UserId = userId,
                    CompanyId = companyId.Value
                });
            }

            return null;
        }
    }
}
