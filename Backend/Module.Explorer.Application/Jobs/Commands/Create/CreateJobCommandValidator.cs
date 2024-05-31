using FluentValidation;
using Module.Explorer.Contracts.Jobs.Commands.Create;

namespace Module.Explorer.Application.Jobs.Commands.Create
{
    public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
    {
        public CreateJobCommandValidator()
        {
            RuleFor(x => x.CompanyId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
