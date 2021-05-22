using System.Reflection;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.AuthService.Infrastructure.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Kwetter.Services.AuthService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var google = new RequestConfig();
            configuration.Bind("Google", google);
            services.AddSingleton(google);
            services.AddScoped<IAuthHttpRequest, GoogleRestClient>();
            return services;
        }
    }
}
