using FluentValidation;
using Module.Companies.Contracts.UserCompanyInvitations.Commands.RespondInvitation;

namespace Module.Companies.Application.UserCompanyInvitations.Commands.RespondInvitation
{
    public class RespondInvitationCommandValidator : AbstractValidator<RespondInvitationCommand>
    {
        public RespondInvitationCommandValidator()
        {
            RuleFor(x => x.CompanyId).GreaterThan(0);
        }
    }
}