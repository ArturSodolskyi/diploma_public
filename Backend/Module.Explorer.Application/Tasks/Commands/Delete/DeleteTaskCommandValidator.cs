using FluentValidation;
using Module.Explorer.Contracts.Tasks.Commands.Delete;

namespace Module.Explorer.Application.Tasks.Commands.Delete
{
    public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}