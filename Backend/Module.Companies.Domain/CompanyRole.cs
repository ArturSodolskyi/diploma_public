namespace Module.Companies.Domain
{
    public class CompanyRole
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<UserCompany>? UserCompanies { get; set; }
    }
}
