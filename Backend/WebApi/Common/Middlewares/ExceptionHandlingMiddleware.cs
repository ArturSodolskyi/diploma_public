using Shared.Exceptions;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace WebApi.Common.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly IWebHostEnvironment _environment;
        public ExceptionHandlingMiddleware(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = GetStatusCode(exception);
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                Message = GetMessage(exception),
                Errors = GetErrors(exception)
            }));
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                ForbiddenException => StatusCodes.Status403Forbidden,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

        private string GetMessage(Exception exception)
        {
            return exception switch
            {
                ValidationException => exception.Message,
                ForbiddenException => exception.Message,
                NotFoundException => exception.Message,
                _ => _environment.IsDevelopment() ? exception.Message : "An error occurred"
            };
        }

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            if (exception is ValidationException validationException)
            {
                return validationException.Errors;
            }

            return ReadOnlyDictionary<string, string[]>.Empty;
        }
    }
}