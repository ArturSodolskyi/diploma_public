using MediatR;

namespace Module.Companies.Contracts.UserCompanies.Queries.Exists
{
    public class ExistsQuery : IRequest<bool>
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
    }
}
