﻿using Microsoft.Extensions.DependencyInjection;
using Shared;
using System.Reflection;

namespace Module.Reviews.Contracts
{
    public static class DependencyInjection
    {
        public static void AddDependensies(this IServiceCollection services)
        {
            services.AddDependensies(Assembly.GetExecutingAssembly());
        }
    }
}
