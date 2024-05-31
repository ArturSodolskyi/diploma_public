using FluentValidation;
using Module.Companies.Contracts.Companies.Commands.Create;

namespace Module.Companies.Application.Companies.Commands.Create
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
