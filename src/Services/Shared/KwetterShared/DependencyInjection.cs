using Kwetter.Services.Shared.Messaging;
using Kwetter.Services.Shared.Messaging.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, string queueName, Action<MessagingBuilder> builderFn = null)
        {
            var builder = new MessagingBuilder(services);

            //Add required services
            services.AddSingleton(new RabbitMQConnector());
            services.AddSingleton(new MessageHandlerRepository(builder.MessageHandlers));
            services.AddHostedService<QueueReaderService>();
            services.AddScoped<IMessagePublisher, MessagePublisher>();
            services.AddSingleton(queueName);
            builderFn?.Invoke(builder);

            //Ensure Queue is available
            IModel _channel = new RabbitMQConnector().CreateChannel();
            _channel.QueueDeclare(queueName, false, false, false, null);
            _channel.QueueBind(queueName, "Kwetter", "", null);

            return services;
        }
    }
}
