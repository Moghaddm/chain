using Chain.Application.Contract.Ports.Services;
using Chain.Application.Interfaces;
using Chain.Application.Services;

namespace Chain.Api.Installers
{
    public static class ApplicationServicesInstaller
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();

            return services;
        }
    }
}
