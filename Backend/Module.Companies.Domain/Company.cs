namespace Module.Companies.Domain
{
    public class Company
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Name { get; set; }

        public ICollection<UserCompany>? UserCompanies { get; set; }
        public ICollection<UserCompanyInvitation>? UserCompanyInvitations { get; set; }
    }
}
