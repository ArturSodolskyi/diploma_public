using FluentValidation;
using Module.Explorer.Contracts.Competencies.Commands.Delete;

namespace Module.Explorer.Application.Jobs.Commands.Delete
{
    public class DeleteJobCommandValidator : AbstractValidator<DeleteCompetenceCommand>
    {
        public DeleteJobCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}