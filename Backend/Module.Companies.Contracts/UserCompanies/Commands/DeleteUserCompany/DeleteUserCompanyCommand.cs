using MediatR;

namespace Module.Companies.Contracts.UserCompanies.Commands.DeleteUserCompany
{
    public class DeleteUserCompanyCommand : IRequest
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
    }
}
