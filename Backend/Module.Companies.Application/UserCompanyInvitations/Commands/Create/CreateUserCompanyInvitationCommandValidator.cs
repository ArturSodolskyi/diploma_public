using FluentValidation;
using Module.Companies.Contracts.UserCompanyInvitations.Commands.Create;

namespace Module.Companies.Application.UserCompanyInvitations.Commands.Create
{
    public class CreateUserCompanyInvitationCommandValidator : AbstractValidator<CreateUserCompanyInvitationCommand>
    {
        public CreateUserCompanyInvitationCommandValidator()
        {
            RuleFor(x => x.CompanyId).GreaterThan(0);
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
