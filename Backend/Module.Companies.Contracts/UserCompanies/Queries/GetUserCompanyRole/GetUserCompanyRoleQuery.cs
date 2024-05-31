using MediatR;
using Module.Companies.Contracts.Common;

namespace Module.Companies.Contracts.UserCompanies.Queries.GetUserCompanyRole
{
    public class GetUserCompanyRoleQuery : IRequest<CompanyRoleEnum?>
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
    }
}
