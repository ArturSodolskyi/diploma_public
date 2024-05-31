using MediatR;
using Module.Users.Contracts.Users.Queries.GetUsers;

namespace Module.Users.Contracts.Users.Queries.GetUsersByIds
{
    public class GetUsersByIdsQuery : IRequest<List<UserViewModel>>
    {
        public required IEnumerable<int> Ids { get; set; }
        public int CompanyId { get; set; }
    }
}
