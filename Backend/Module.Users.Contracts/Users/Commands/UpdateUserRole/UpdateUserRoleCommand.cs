using MediatR;
using Shared.Models;

namespace Module.Users.Contracts.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommand : IRequest
    {
        public int UserId { get; set; }
        public RoleEnum Role { get; set; }
    }
}