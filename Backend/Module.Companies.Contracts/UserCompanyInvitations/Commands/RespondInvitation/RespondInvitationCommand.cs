using MediatR;

namespace Module.Companies.Contracts.UserCompanyInvitations.Commands.RespondInvitation
{
    public class RespondInvitationCommand : IRequest
    {
        public int CompanyId { get; set; }
        public bool Accept { get; set; }
    }
}
