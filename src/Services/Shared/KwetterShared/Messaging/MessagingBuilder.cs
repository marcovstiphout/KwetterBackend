using Kwetter.Services.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kwetter.Services.Shared.Messaging
{
    public class MessagingBuilder
    {
        /// <summary>
        ///  The underlying service collection to register message handlers to
        /// </summary>
        private readonly IServiceCollection _services;

        /// <summary>
        /// the message handlers that are registered
        /// </summary>
        private Dictionary<string, Type> _messageHandlers = new Dictionary<string, Type>();

        /// <summary>
        /// Readonly variant of all the message handlers registered by the <see cref="MessagingBuilder"/>
        /// </summary>
        internal IReadOnlyDictionary<string, Type> MessageHandlers => new ReadOnlyDictionary<string, Type>(_messageHandlers);

        internal MessagingBuilder(IServiceCollection services)
        {
            _services = services;
        }

        /// <summary>
        /// Registers <typeparamref name="THandler"/> as the handler for a message of the given <paramref name="messageType"/>
        /// </summary>
        /// <typeparam name="THandler">The handler implementing <see cref="IMessageHandler"/></typeparam>
        /// <param name="messageType">The message type that this handlers handles</param>
        public MessagingBuilder WithHandler<THandler>(string messageType)
            where THandler : IMessageHandler
        {
            var type = typeof(THandler);
            _services.AddScoped(type);
            _messageHandlers.Add(messageType, type);
            return this;
        }
    }
}
