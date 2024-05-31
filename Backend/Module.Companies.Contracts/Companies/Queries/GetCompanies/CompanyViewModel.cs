using Module.Companies.Contracts.Common;

namespace Module.Companies.Contracts.Companies.Queries.GetCompanies
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsCreatedByCurrentUser { get; set; }
        public CompanyRoleEnum Role { get; set; }
    }
}
