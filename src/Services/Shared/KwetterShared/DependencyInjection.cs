using Kwetter.Services.Shared.Messaging;
using Kwetter.Services.Shared.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KwetterShared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, string queueName, Action<MessagingBuilder> builderFn = null)
        {
            var builder = new MessagingBuilder(services);

            services.AddSingleton(new RabbitMQConnector());
            services.AddSingleton(new MessageHandlerRepository(builder.MessageHandlers));
            services.AddHostedService<QueueReaderService>();
            services.AddScoped<IMessagePublisher, MessagePublisher>();
            services.AddSingleton(queueName);
            builderFn?.Invoke(builder);

            return services;
        }
    }
}
