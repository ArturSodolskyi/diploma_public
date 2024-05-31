using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Commands.UpdateRole;
using Module.Companies.Persistence;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Companies.Application.UserCompanies.Commands.UpdateRole
{
    public class UpdateRoleCommandAuthorizer : IAuthorizer<UpdateRoleCommand>
    {
        private readonly ICompaniesDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        public UpdateRoleCommandAuthorizer(ICompaniesDbContext dbContext,
            IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task AuthorizeAsync(UpdateRoleCommand request, CancellationToken cancellationToken = default)
        {
            var userCompany = await _dbContext.UserCompanies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == request.UserId
                    && x.CompanyId == request.CompanyId, cancellationToken);

            if (userCompany is null)
            {
                throw new ForbiddenException();
            }

            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var requestorCompany = await _dbContext.UserCompanies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == _userAccessor.UserId
                    && x.CompanyId == request.CompanyId, cancellationToken);

            if (requestorCompany is null || requestorCompany.RoleId != (int)CompanyRoleEnum.Administrator)
            {
                throw new ForbiddenException();
            }

            if (requestorCompany.RoleId == (int)CompanyRoleEnum.Administrator)
            {
                return;
            }

            if (request.Role == CompanyRoleEnum.Administrator)
            {
                throw new ForbiddenException();
            }
        }
    }
}