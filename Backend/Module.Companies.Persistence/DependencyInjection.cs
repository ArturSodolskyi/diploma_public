using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Module.Companies.Persistence.Common;

namespace Module.Companies.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CompaniesDbContext>(options => options.UseSqlServer(
                connectionString,
                action => action.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema.COMPANIES)));
            services.AddTransient<ICompaniesDbContext>(provider => provider.GetService<CompaniesDbContext>()!);
            return services;
        }

        public static void EnsurePersistence(this IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<CompaniesDbContext>();
            context.Database.Migrate();
        }
    }
}
