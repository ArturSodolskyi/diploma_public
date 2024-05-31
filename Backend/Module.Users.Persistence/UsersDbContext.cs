using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Module.Users.Domain;
using Module.Users.Persistence.Common.Extensions;

namespace Module.Users.Persistence
{
    public class UsersDbContext(DbContextOptions<UsersDbContext> options) : IdentityDbContext<User, UserRole, int>(options), IUsersDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<IdentityUserRole<int>> UsersRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("users");
            builder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);
            builder.SeedAdmins();
            base.OnModelCreating(builder);
        }
    }
}
