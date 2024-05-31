using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Users.Domain;
using Shared.Extensions;
using Shared.Models;

namespace Module.Users.Persistence.EntityConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasData(
                GetUserRole(RoleEnum.User),
                GetUserRole(RoleEnum.Administrator)
            );
        }

        private static UserRole GetUserRole(RoleEnum role)
        {
            var name = role.GetEnumDescription();
            return new UserRole
            {
                Id = (int)role,
                Name = name,
                NormalizedName = name.ToUpper(),
            };
        }
    }
}
