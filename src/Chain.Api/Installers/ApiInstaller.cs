
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Chain.Api.Installers
{
    public static class ApiInstaller
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {

            services.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Chain E-Commerce APIs",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "mahdimoghaddam2005@gmail.com",
                        Name = "Mohammad Mahdi Moghaddam",
                        Url = new Uri("https://moghaddam.bio.link")
                    },
                    Description = "Simple E-Commerce Management (Chain Commerce)"
                });
            });
            services.AddApiVersioning(versionBuilder =>
            {
                versionBuilder.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                versionBuilder.AssumeDefaultVersionWhenUnspecified = true;
                versionBuilder.ReportApiVersions = true;
                versionBuilder.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            services.AddVersionedApiExplorer(apiExplorer =>
            {
                apiExplorer.GroupNameFormat = "'v'VVV";
                apiExplorer.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
