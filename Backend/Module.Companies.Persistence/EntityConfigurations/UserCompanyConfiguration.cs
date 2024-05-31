using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Companies.Domain;

namespace Module.Companies.Persistence.EntityConfigurations
{
    public class UserCompanyConfiguration : IEntityTypeConfiguration<UserCompany>
    {
        public void Configure(EntityTypeBuilder<UserCompany> builder)
        {
            builder.HasKey(x => new { x.UserId, x.CompanyId });

            builder.HasOne(x => x.Company)
                .WithMany(x => x.UserCompanies)
                .HasForeignKey(x => x.CompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CompanyRole)
                .WithMany(x => x.UserCompanies)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();
        }
    }
}