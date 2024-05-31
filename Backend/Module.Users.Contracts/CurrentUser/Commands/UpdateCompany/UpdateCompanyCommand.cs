using MediatR;

namespace Module.Users.Contracts.CurrentUser.Commands.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest
    {
        public int CompanyId { get; set; }
    }
}