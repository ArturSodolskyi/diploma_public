namespace WebApi.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            Module.Companies.Persistence.DependencyInjection.AddPersistence(services, connectionString);
            Module.Explorer.Persistence.DependencyInjection.AddPersistence(services, connectionString);
            Module.Reviews.Persistence.DependencyInjection.AddPersistence(services, connectionString);
            Module.Users.Persistence.DependencyInjection.AddPersistence(services, connectionString);
            return services;
        }

        public static void EnsurePersistence(this IServiceProvider serviceProvider)
        {
            Module.Companies.Persistence.DependencyInjection.EnsurePersistence(serviceProvider);
            Module.Explorer.Persistence.DependencyInjection.EnsurePersistence(serviceProvider);
            Module.Reviews.Persistence.DependencyInjection.EnsurePersistence(serviceProvider);
            Module.Users.Persistence.DependencyInjection.EnsurePersistence(serviceProvider);
        }

        public static void AddModuleDependencies(this IServiceCollection services)
        {
            Module.Companies.Contracts.DependencyInjection.AddDependensies(services);
            Module.Explorer.Contracts.DependencyInjection.AddDependensies(services);
            Module.Reviews.Contracts.DependencyInjection.AddDependensies(services);
            Module.Users.Contracts.DependencyInjection.AddDependensies(services);

            Module.Companies.Application.DependencyInjection.AddDependensies(services);
            Module.Explorer.Application.DependencyInjection.AddDependensies(services);
            Module.Reviews.Application.DependencyInjection.AddDependensies(services);
            Module.Users.Application.DependencyInjection.AddDependensies(services);
        }
    }
}
