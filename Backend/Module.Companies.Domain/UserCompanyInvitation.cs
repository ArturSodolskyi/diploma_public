namespace Module.Companies.Domain
{
    public class UserCompanyInvitation
    {
        public required string Email { get; set; }

        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
