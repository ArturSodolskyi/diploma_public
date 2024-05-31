using Microsoft.Extensions.DependencyInjection;
using Module.Companies.Application;
using Module.Companies.Contracts;
using Shared;
using System.Reflection;

namespace Module.Companies.Application
{
    public static class DependencyInjection
    {
        public static void AddDependensies(this IServiceCollection services)
        {
            services.AddDependensies(Assembly.GetExecutingAssembly());
        }
    }
}
