using FluentValidation;
using Module.Explorer.Contracts.Tasks.Commands.Create;

namespace Module.Explorer.Application.Tasks.Commands.Create
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.CompetenceId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
