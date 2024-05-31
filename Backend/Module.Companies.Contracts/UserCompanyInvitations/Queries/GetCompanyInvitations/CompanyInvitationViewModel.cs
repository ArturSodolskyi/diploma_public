namespace Module.Companies.Contracts.UserCompanyInvitations.Queries.GetCompanyInvitations
{
    public class CompanyInvitationViewModel
    {
        public int CompanyId { get; set; }
        public required string CompanyName { get; set; }
    }
}
