using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Companies.Domain;

namespace Module.Companies.Persistence.EntityConfigurations
{
    public class UserCompanyInvitationConfiguration : IEntityTypeConfiguration<UserCompanyInvitation>
    {
        public void Configure(EntityTypeBuilder<UserCompanyInvitation> builder)
        {
            builder.HasKey(x => new { x.Email, x.CompanyId });

            builder.HasOne(x => x.Company)
                .WithMany(x => x.UserCompanyInvitations)
                .HasForeignKey(x => x.CompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}