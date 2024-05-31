using FluentValidation;
using Module.Users.Contracts.CurrentUser.Commands.UpdateCompany;

namespace Module.Users.Application.CurrentUser.Commands.UpdateCompany
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.CompanyId).GreaterThan(0);
        }
    }
}
