using FluentValidation;
using Module.Explorer.Contracts.Jobs.Commands.Update;

namespace Module.Explorer.Application.Jobs.Commands.Update
{
    public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
    {
        public UpdateJobCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}