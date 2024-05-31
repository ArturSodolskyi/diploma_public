using FluentValidation;
using Module.Explorer.Contracts.Categories.Commands.Delete;

namespace Module.Explorer.Application.Categories.Commands.Delete
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}