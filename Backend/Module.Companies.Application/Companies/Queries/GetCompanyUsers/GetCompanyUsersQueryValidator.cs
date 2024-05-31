using FluentValidation;
using Module.Companies.Contracts.Companies.Queries.GetUsers;

namespace Module.Companies.Application.Companies.Queries.GetCompanyUsers
{
    public class GetCompanyUsersQueryValidator : AbstractValidator<GetCompanyUsersQuery>
    {
        public GetCompanyUsersQueryValidator()
        {
            RuleFor(x => x.CompanyId).GreaterThan(0);
        }
    }
}