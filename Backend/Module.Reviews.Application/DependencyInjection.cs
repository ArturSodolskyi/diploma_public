using Microsoft.Extensions.DependencyInjection;
using Module.Companies.Contracts;
using Module.Reviews.Application;
using Shared;
using System.Reflection;

namespace Module.Reviews.Application
{
    public static class DependencyInjection
    {
        public static void AddDependensies(this IServiceCollection services)
        {
            services.AddDependensies(Assembly.GetExecutingAssembly());
        }
    }
}
