using Module.Companies.Contracts.Common;

namespace Module.Companies.Contracts.Companies.Queries.GetCompanyUsers
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public CompanyRoleEnum Role { get; set; }
    }
}
