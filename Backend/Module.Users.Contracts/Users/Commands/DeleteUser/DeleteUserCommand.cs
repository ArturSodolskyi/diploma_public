using MediatR;

namespace Module.Users.Contracts.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public int UserId { get; set; }
    }
}