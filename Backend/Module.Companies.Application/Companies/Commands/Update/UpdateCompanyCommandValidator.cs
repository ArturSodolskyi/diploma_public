using FluentValidation;
using Module.Companies.Contracts.Companies.Commands.Update;

namespace Module.Companies.Application.Companies.Commands.Update
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}