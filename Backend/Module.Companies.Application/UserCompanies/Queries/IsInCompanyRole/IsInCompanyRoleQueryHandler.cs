using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Module.Companies.Persistence;

namespace Module.Companies.Application.UserCompanies.Queries.IsInCompanyRole
{
    public class IsInCompanyRoleQueryHandler : IRequestHandler<IsInCompanyRoleQuery, bool>
    {
        private readonly ICompaniesDbContext _dbContext;
        public IsInCompanyRoleQueryHandler(ICompaniesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(IsInCompanyRoleQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.UserCompanies
                .AnyAsync(x => x.UserId == request.UserId
                    && x.CompanyId == request.CompanyId
                    && x.RoleId == (int)request.Role,
                    cancellationToken: cancellationToken);
        }
    }
}
