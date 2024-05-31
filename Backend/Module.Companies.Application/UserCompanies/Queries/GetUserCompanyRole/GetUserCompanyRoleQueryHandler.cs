using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Queries.GetUserCompanyRole;
using Module.Companies.Persistence;

namespace Module.Companies.Application.UserCompanies.Queries.GetUserCompanyRole
{
    public class GetUserCompanyRoleQueryHandler : IRequestHandler<GetUserCompanyRoleQuery, CompanyRoleEnum?>
    {
        private readonly ICompaniesDbContext _dbContext;
        public GetUserCompanyRoleQueryHandler(ICompaniesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CompanyRoleEnum?> Handle(GetUserCompanyRoleQuery request, CancellationToken cancellationToken)
        {
            var userCompany = await _dbContext.UserCompanies
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.CompanyId == request.CompanyId, cancellationToken);
            return (CompanyRoleEnum?)userCompany?.RoleId;
        }
    }
}
