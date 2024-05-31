using FluentValidation;
using Module.Explorer.Contracts.Categories.Commands.Create;

namespace Module.Explorer.Application.Categories.Commands.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CompanyId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
