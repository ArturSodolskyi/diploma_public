using FluentValidation;
using Module.Explorer.Contracts.Competencies.Commands.Update;

namespace Module.Explorer.Application.Competencies.Commands.Update
{
    public class UpdateCompetenceCommandValidator : AbstractValidator<UpdateCompetenceCommand>
    {
        public UpdateCompetenceCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}