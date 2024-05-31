using Microsoft.Extensions.DependencyInjection;
using Module.Companies.Contracts;
using Module.Users.Application;
using Shared;
using System.Reflection;

namespace Module.Users.Application
{
    public static class DependencyInjection
    {
        public static void AddDependensies(this IServiceCollection services)
        {
            services.AddDependensies(Assembly.GetExecutingAssembly());
        }
    }
}
