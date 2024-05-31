using Microsoft.EntityFrameworkCore;
using Module.Companies.Domain;

namespace Module.Companies.Persistence
{
    public interface ICompaniesDbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyRole> CompanyRoles { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<UserCompanyInvitation> UserCompanyInvitations { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
