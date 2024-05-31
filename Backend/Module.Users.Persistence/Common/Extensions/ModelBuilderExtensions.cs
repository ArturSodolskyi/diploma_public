using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.Users.Domain;
using Shared.Models;

namespace Module.Users.Persistence.Common.Extensions
{
    public static class ModelBuilderExtensions
    {
        //TODO: delete when finish and seed manually
        public static void SeedAdmins(this ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                Id = 1,
                FirstName = "Artur",
                LastName = "Sodolskyi",
                UserName = "next.tmz.mit@gmail.com",
                Email = "next.tmz.mit@gmail.com",
                NormalizedUserName = "next.tmz.mit@gmail.com".ToUpper(),
                NormalizedEmail = "next.tmz.mit@gmail.com".ToUpper(),
                PasswordHash = hasher.HashPassword(null!, "Test12345!"),
                EmailConfirmed = true,
                LockoutEnabled = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            builder.Entity<User>().HasData(user);

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = user.Id, RoleId = (int)RoleEnum.Administrator });
        }
    }
}
