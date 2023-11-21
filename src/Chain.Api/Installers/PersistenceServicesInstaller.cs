
using Chain.Application.Contract.Ports.Repositories;
using Chain.Infrastructure.Persistence;
using Chain.Infrastructure.Repositories;
using Chain.Persistence.Repositories;

namespace Chain.Api.Installers
{
    public static class PersistenceServicesInstaller
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ChainDbContext>();
            services.AddScoped<IUnitOfWork, ChainDbContext>();
            services.AddTransient<IProductRepository, ProductEfRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
