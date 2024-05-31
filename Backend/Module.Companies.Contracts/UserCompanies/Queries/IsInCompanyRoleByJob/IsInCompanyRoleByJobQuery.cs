using MediatR;
using Module.Companies.Contracts.Common;

namespace Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRoleByJob
{
    public class IsInCompanyRoleByJobQuery : IRequest<bool>
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public CompanyRoleEnum Role { get; set; }
    }
}
