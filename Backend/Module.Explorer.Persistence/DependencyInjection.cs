using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Module.Explorer.Persistence.Common;

namespace Module.Explorer.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ExplorerDbContext>(options => options.UseSqlServer(
                connectionString,
                action => action.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema.EXPLORER)));
            services.AddTransient<IExplorerDbContext>(provider => provider.GetService<ExplorerDbContext>()!);
            return services;
        }

        public static void EnsurePersistence(this IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ExplorerDbContext>();
            context.Database.Migrate();
        }
    }
}
