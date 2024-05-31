using FluentValidation;
using Module.Companies.Contracts.Companies.Commands.Delete;

namespace Module.Companies.Application.Companies.Commands.Delete
{
    public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
    {
        public DeleteCompanyCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}