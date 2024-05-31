using MediatR;
using Module.Companies.Contracts.Common;

namespace Module.Companies.Contracts.UserCompanies.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public CompanyRoleEnum Role { get; set; }
    }
}
