using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Module.Reviews.Persistence.Common;

namespace Module.Reviews.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ReviewsDbContext>(options => options.UseSqlServer(
                connectionString,
                action => action.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema.REVIEWS)));
            services.AddTransient<IReviewsDbContext>(provider => provider.GetService<ReviewsDbContext>()!);
            return services;
        }

        public static void EnsurePersistence(this IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ReviewsDbContext>();
            context.Database.Migrate();
        }
    }
}
