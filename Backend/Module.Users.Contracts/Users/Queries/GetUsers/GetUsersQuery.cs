using MediatR;

namespace Module.Users.Contracts.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<List<UserViewModel>>
    {
    }
}
