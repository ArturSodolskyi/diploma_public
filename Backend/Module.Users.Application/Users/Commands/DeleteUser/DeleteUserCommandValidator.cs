using FluentValidation;
using Module.Users.Contracts.Users.Commands.DeleteUser;

namespace Module.Users.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}