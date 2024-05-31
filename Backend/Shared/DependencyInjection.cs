using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Accessors;
using Shared.Behaviors;
using Shared.Extensions;
using Shared.Mapping;
using System.Reflection;

namespace Shared
{
    public static class DependencyInjection
    {
        public static void AddPipelines(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        }

        public static void AddDependensies(this IServiceCollection services, Assembly assembly)
        {
            services.AddTransient<IUserAccessor, UserAccessor>();
            services.AddAutoMapper(x => x.AddProfile(new AssemblyMappingProfile(assembly)));
            services.AddMediatR(x => x.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssemblies([assembly]);
            services.AddAuthorizersFromAssembly(assembly);
        }
    }
}
