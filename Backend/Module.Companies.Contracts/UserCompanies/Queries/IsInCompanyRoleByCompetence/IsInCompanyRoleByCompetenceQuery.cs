using MediatR;
using Module.Companies.Contracts.Common;

namespace Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRoleByCompetence
{
    public class IsInCompanyRoleByCompetenceQuery : IRequest<bool>
    {
        public int CompetenceId { get; set; }
        public int UserId { get; set; }
        public CompanyRoleEnum Role { get; set; }
    }
}
