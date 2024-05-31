using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Companies.Contracts.Common;
using Module.Companies.Domain;
using Shared.Extensions;

namespace Module.Companies.Persistence.EntityConfigurations
{
    public class CompanyRoleConfiguration : IEntityTypeConfiguration<CompanyRole>
    {
        public void Configure(EntityTypeBuilder<CompanyRole> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasData(
                GetCompanyRole(CompanyRoleEnum.Administrator),
                GetCompanyRole(CompanyRoleEnum.Watcher)
            );
        }

        private static CompanyRole GetCompanyRole(CompanyRoleEnum role)
        {
            return new CompanyRole
            {
                Id = (int)role,
                Name = role.GetEnumDescription(),
            };
        }
    }
}
