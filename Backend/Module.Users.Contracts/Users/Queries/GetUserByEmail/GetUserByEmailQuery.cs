using MediatR;
using Module.Users.Contracts.Users.Queries.GetUsers;

namespace Module.Users.Contracts.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserViewModel?>
    {
        public required string Email { get; set; }
    }
}
