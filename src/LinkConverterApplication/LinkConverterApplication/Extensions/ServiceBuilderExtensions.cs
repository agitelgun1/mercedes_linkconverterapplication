using LinkConverterApplication.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using LinkConverterApplication.Domain.Abstractions;
using LinkConverterApplication.Domain.Services;
using LinkConverterApplication.Helpers.Absractions;
using LinkConverterApplication.Helpers.Helpers;
using LinkConverterApplication.Repositories;
using Microsoft.Extensions.Configuration;

namespace LinkConverterApplication.Extensions
{
    public static class ServiceBuilderExtensions
    {
        public static void ConfigureSwaggerGenCollection(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "NttDataLinkConverterApplication", Version = "v1"});
            });
        }

        public static void ConfigureSingletonCollections(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IConnectionHelper, ConnectionHelper>();
            serviceCollection.AddSingleton<IConvertLinkService, ConvertLinkService>();
        }
        
        public static void AddInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IWebUrlRepository, WebUrlRepository>();
            serviceCollection.AddTransient<IErrorRepository, ErrorRepository>();
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
        }
        
        public static void ConfigureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        }
    }
}