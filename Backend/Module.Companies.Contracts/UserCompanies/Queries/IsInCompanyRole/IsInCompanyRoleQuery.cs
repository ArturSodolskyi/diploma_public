using MediatR;
using Module.Companies.Contracts.Common;

namespace Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole
{
    public class IsInCompanyRoleQuery : IRequest<bool>
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public CompanyRoleEnum Role { get; set; }
    }
}
