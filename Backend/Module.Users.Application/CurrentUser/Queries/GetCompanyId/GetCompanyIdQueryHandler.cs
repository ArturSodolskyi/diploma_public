using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Users.Contracts.CurrentUser.Queries.GetCompanyId;
using Module.Users.Persistence;
using Shared.Accessors;

namespace Module.Users.Application.CurrentUser.Queries.GetCompanyId
{
    public class GetCompanyIdQueryHandler : IRequestHandler<GetCompanyIdQuery, int?>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        public GetCompanyIdQueryHandler(IUsersDbContext dbContext,
            IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task<int?> Handle(GetCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .SingleAsync(x => x.Id == _userAccessor.UserId, cancellationToken);

            return user.CompanyId;
        }
    }
}
