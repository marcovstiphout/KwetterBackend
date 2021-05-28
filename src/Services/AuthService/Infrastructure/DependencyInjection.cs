using System;
using System.Reflection;
using System.Text;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Kwetter.Services.AuthService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
