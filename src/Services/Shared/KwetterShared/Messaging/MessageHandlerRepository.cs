using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.Shared.Messaging
{
    internal class MessageHandlerRepository
    {
        private readonly IReadOnlyDictionary<string, Type> _messageHandlers;

        internal MessageHandlerRepository(IReadOnlyDictionary<string, Type> messageHandlers)
        {
            _messageHandlers = messageHandlers;
        }

        public bool TryGetHandlerForMessageType(string messageType, out Type type)
        {
            return _messageHandlers.TryGetValue(messageType, out type);
        }
    }
}
