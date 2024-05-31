using MediatR;

namespace Module.Companies.Contracts.Companies.Queries.GetCompanies
{
    public class GetCompaniesQuery : IRequest<List<CompanyViewModel>>
    {

    }
}
