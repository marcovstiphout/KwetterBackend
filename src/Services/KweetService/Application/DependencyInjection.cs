using Kwetter.Services.KweetService.Application.Common.Interfaces.Services;
using Kwetter.Services.KweetService.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IKweetService, KweetService>();

            return services;
        }
    }
}
