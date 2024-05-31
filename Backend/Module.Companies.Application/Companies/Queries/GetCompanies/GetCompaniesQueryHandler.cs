using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.Companies.Queries.GetCompanies;
using Module.Companies.Persistence;
using Shared.Accessors;

namespace Module.Companies.Application.Companies.Queries.GetCompanies
{
    public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, List<CompanyViewModel>>
    {
        private readonly ICompaniesDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        public GetCompaniesQueryHandler(ICompaniesDbContext dbContext,
            IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task<List<CompanyViewModel>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.UserCompanies
                .Where(x => x.UserId == _userAccessor.UserId)
                .Include(x => x.Company)
                .Select(x => new CompanyViewModel
                {
                    Id = x.CompanyId,
                    Name = x.Company!.Name,
                    IsCreatedByCurrentUser = x.Company.UserId == _userAccessor.UserId,
                    Role = (CompanyRoleEnum)x.RoleId
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
