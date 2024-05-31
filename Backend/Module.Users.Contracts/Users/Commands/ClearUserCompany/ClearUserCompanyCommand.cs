using MediatR;

namespace Module.Users.Contracts.Users.Commands.ClearUserCompany
{
    public class ClearUserCompanyCommand : IRequest
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
    }
}
