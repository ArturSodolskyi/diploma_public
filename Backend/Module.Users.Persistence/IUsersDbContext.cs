using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.Users.Domain;

namespace Module.Users.Persistence
{
    public interface IUsersDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<IdentityUserRole<int>> UsersRoles { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
