using Module.Companies.Contracts.Common;
using Shared.Models;

namespace Module.Users.Contracts.CurrentUser.Queries.GetUser
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public RoleEnum Role { get; set; }
        public int? CompanyId { get; set; }
        public CompanyRoleEnum? CompanyRole { get; set; }
    }
}
