using MediatR;
using Module.Users.Contracts.Users.Queries.GetUsers;

namespace Module.Users.Contracts.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserViewModel>
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
    }
}
