using Microsoft.AspNetCore.Builder;
using LinkConverterApplication.Middleware;

namespace LinkConverterApplication.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSwaggerUiBuilder(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinkConverterApplication v1"));
        }

        public static void UseMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ErrorMiddleware>();
        }
    }
}