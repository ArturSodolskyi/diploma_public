using FluentValidation;
using Module.Explorer.Contracts.Tasks.Commands.Update;

namespace Module.Explorer.Application.Tasks.Commands.Update
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).NotNull();
        }
    }
}
