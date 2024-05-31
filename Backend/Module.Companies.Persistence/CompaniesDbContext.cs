using Microsoft.EntityFrameworkCore;
using Module.Companies.Domain;
using Module.Companies.Persistence.Common;

namespace Module.Companies.Persistence
{
    public class CompaniesDbContext(DbContextOptions<CompaniesDbContext> options) : DbContext(options), ICompaniesDbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyRole> CompanyRoles { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<UserCompanyInvitation> UserCompanyInvitations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema.COMPANIES);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompaniesDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
