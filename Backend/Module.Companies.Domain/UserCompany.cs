namespace Module.Companies.Domain
{
    public class UserCompany
    {
        public int UserId { get; set; }

        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public int RoleId { get; set; }
        public CompanyRole? CompanyRole { get; set; }
    }
}
