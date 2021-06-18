using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDatabaseSettings = Kwetter.Services.KweetService.Persistence.Contexts.MongoDatabaseSettings;

namespace Kwetter.Services.KweetService.Persistence
{
    public static class MongoDBInjection
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            // requires using Microsoft.Extensions.Options
            services.Configure<MongoDatabaseSettings>(
                configuration.GetSection("MongoDatabaseSettings"));

            services.AddSingleton<IMongoDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);
            return services;
        }
    }
}
