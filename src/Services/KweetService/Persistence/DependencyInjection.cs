using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.KweetServioce.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KweetContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString"))
                .UseLazyLoadingProxies()
            );
            services.AddScoped<IKweetContext>(provider => provider.GetService<KweetContext>());

            return services;
        }
    }
}
