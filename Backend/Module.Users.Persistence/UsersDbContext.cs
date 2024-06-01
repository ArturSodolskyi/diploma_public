using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Module.Users.Domain;

namespace Module.Users.Persistence
{
    public class UsersDbContext(DbContextOptions<UsersDbContext> options) : IdentityDbContext<User, UserRole, int>(options), IUsersDbContext
    {
        public new DbSet<User> Users { get; set; }
        public new DbSet<UserRole> UserRoles { get; set; }
        public DbSet<IdentityUserRole<int>> UsersRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("users");
            builder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
