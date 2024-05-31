using FluentValidation;
using Module.Explorer.Contracts.Competencies.Commands.Delete;

namespace Module.Explorer.Application.Competencies.Commands.Delete
{
    public class DeleteCompetenceCommandValidator : AbstractValidator<DeleteCompetenceCommand>
    {
        public DeleteCompetenceCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}