using MediatR;

namespace Module.Users.Contracts.Users.Commands.ClearCompany
{
    public class ClearCompanyCommand : IRequest
    {
        public int CompanyId { get; set; }
    }
}
