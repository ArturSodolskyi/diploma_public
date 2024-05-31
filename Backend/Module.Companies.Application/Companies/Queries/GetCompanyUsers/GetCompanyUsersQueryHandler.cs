using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.Companies.Queries.GetCompanyUsers;
using Module.Companies.Contracts.Companies.Queries.GetUsers;
using Module.Companies.Persistence;
using Module.Users.Contracts.Users.Queries.GetUsersByIds;

namespace Module.Companies.Application.Companies.Queries.GetCompanyUsers
{
    public class GetCompanyUsersQueryHandler : IRequestHandler<GetCompanyUsersQuery, List<UserViewModel>>
    {
        private readonly ICompaniesDbContext _dbContext;
        private readonly IMediator _mediator;
        public GetCompanyUsersQueryHandler(ICompaniesDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<List<UserViewModel>> Handle(GetCompanyUsersQuery request, CancellationToken cancellationToken)
        {
            var userCompanies = await _dbContext.UserCompanies
                .Where(x => x.CompanyId == request.CompanyId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var users = await _mediator.Send(new GetUsersByIdsQuery
            {
                Ids = userCompanies.Select(x => x.UserId),
                CompanyId = request.CompanyId
            }, cancellationToken);

            return userCompanies
                .Join(users, uc => uc.UserId, u => u.Id, (uc, u) => new UserViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email!,
                    Role = (CompanyRoleEnum)uc.RoleId
                })
                .ToList();
        }
    }
}
