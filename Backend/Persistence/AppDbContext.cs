using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfigurations;
using Persistence.EntityTypeConfigurations;
using Persistence.Extensions;

namespace Persistence
{
    public class AppDbContext : IdentityDbContext<User, UserRole, int>, IAppDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyRole> CompanyRoles { get; set; }
        public DbSet<Competence> Competencies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewResult> ReviewResults { get; set; }
        public DbSet<ReviewTask> ReviewTasks { get; set; }
        public DbSet<CompetenceTask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<IdentityUserRole<int>> UsersRoles { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<UserCompanyInvitation> UserCompanyInvitations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CompanyConfiguration());
            builder.ApplyConfiguration(new CompanyRoleConfiguration());
            builder.ApplyConfiguration(new CompetenceConfiguration());
            builder.ApplyConfiguration(new JobConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
            builder.ApplyConfiguration(new ReviewResultConfiguration());
            builder.ApplyConfiguration(new ReviewTaskConfiguration());
            builder.ApplyConfiguration(new TaskConfiguration());
            builder.ApplyConfiguration(new UserCompanyConfiguration());
            builder.ApplyConfiguration(new UserCompanyInvitationConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

            builder.SeedAdmins();

            base.OnModelCreating(builder);
        }
    }
}
