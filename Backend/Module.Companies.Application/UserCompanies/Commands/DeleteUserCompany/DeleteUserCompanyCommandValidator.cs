using FluentValidation;
using Module.Companies.Contracts.UserCompanies.Commands.DeleteUserCompany;

namespace Module.Companies.Application.UserCompanies.Commands.DeleteUserCompany
{
    public class DeleteUserCompanyCommandValidator : AbstractValidator<DeleteUserCompanyCommand>
    {
        public DeleteUserCompanyCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.CompanyId).GreaterThan(0);
        }
    }
}