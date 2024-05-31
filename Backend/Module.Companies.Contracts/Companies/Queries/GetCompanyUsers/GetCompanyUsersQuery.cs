using MediatR;
using Module.Companies.Contracts.Companies.Queries.GetCompanyUsers;

namespace Module.Companies.Contracts.Companies.Queries.GetUsers
{
    public class GetCompanyUsersQuery : IRequest<List<UserViewModel>>
    {
        public int CompanyId { get; set; }
    }
}
