using FluentValidation;
using Module.Explorer.Contracts.Competencies.Commands.Create;

namespace Module.Explorer.Application.Competencies.Commands.Create
{
    public class CreateCompetenceCommandValidator : AbstractValidator<CreateCompetenceCommand>
    {
        public CreateCompetenceCommandValidator()
        {
            RuleFor(x => x.JobId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
