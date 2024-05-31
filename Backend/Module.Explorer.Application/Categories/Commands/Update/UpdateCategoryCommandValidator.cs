using FluentValidation;
using Module.Explorer.Contracts.Categories.Commands.Update;

namespace Module.Explorer.Application.Categories.Commands.Update
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}