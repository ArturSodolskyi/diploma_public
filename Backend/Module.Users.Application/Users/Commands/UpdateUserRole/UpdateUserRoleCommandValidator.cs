using FluentValidation;
using Module.Users.Contracts.Users.Commands.UpdateUserRole;

namespace Module.Users.Application.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandValidator : AbstractValidator<UpdateUserRoleCommand>
    {
        public UpdateUserRoleCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.Role).IsInEnum();
        }
    }
}