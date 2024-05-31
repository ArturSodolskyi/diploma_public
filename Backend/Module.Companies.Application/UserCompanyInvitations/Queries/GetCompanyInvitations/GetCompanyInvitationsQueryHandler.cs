using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.UserCompanyInvitations.Queries.GetCompanyInvitations;
using Module.Companies.Persistence;
using Shared.Accessors;

namespace Module.Companies.Application.UserCompanyInvitations.Queries.GetCompanyInvitations
{
    public class GetCompanyInvitationsQueryHandler : IRequestHandler<GetCompanyInvitationsQuery, List<CompanyInvitationViewModel>>
    {
        private readonly ICompaniesDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        public GetCompanyInvitationsQueryHandler(ICompaniesDbContext dbContext,
            IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task<List<CompanyInvitationViewModel>> Handle(GetCompanyInvitationsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.UserCompanyInvitations
                .Include(x => x.Company)
                .Where(x => x.Email == _userAccessor.Email)
                .Select(x => new CompanyInvitationViewModel
                {
                    CompanyId = x.CompanyId,
                    CompanyName = x.Company.Name
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}