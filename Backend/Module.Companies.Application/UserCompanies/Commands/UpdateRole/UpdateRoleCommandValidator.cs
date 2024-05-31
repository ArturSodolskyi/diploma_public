using FluentValidation;
using Module.Companies.Contracts.UserCompanies.Commands.UpdateRole;

namespace Module.Companies.Application.UserCompanies.Commands.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.CompanyId).GreaterThan(0);
            RuleFor(x => x.Role).IsInEnum();
        }
    }
}