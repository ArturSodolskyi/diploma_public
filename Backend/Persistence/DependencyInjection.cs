using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection
            services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:Db"];
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IAppDbContext>(provider => provider.GetService<AppDbContext>());
            return services;
        }
    }
}
