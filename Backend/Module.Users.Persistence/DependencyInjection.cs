using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Module.Users.Domain;
using Module.Users.Persistence.Common;

namespace Module.Users.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(
                connectionString,
                action => action.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema.USERS)));
            services.AddTransient<IUsersDbContext>(provider => provider.GetService<UsersDbContext>()!);

            services.AddIdentityCore<User>()
                .AddRoles<UserRole>()
                .AddEntityFrameworkStores<UsersDbContext>();

            return services;
        }

        public static void EnsurePersistence(this IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<UsersDbContext>();
            context.Database.Migrate();
        }
    }
}
