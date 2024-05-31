using MediatR;

namespace Module.Companies.Contracts.UserCompanyInvitations.Commands.Create
{
    public class CreateUserCompanyInvitationCommand : IRequest
    {
        public required string Email { get; set; }
        public int CompanyId { get; set; }
    }
}
