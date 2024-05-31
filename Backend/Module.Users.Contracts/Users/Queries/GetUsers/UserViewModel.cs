using Shared.Models;

namespace Module.Users.Contracts.Users.Queries.GetUsers
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public RoleEnum Role { get; set; }
    }
}
