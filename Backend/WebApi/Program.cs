using Microsoft.OpenApi.Models;
using Module.Users.Domain;
using Shared;
using Shared.Mapping;
using System.Reflection;
using WebApi.Common.Extensions;
using WebApi.Common.Middlewares;
using WebApi.Common.Models;

namespace WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            });

            builder.Services.AddTransient<ExceptionHandlingMiddleware>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddPipelines();
            builder.Services.AddModuleDependencies();

            var connectionString = builder.Configuration.GetConnectionString("Db")!;
            builder.Services.AddPersistence(connectionString);

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(Cors.UI, policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.WithOrigins(builder.Configuration.GetValue<string>("Origins")!);
                });
            });

            builder.Services.AddAuthentication().AddBearerToken();
            builder.Services.AddAuthorizationBuilder();
            builder.Services.AddIdentityApiEndpoints<User>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter your token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            scope.ServiceProvider.EnsurePersistence();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseCors(Cors.UI);
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers().RequireAuthorization();

            app.Run();
        }
    }
}