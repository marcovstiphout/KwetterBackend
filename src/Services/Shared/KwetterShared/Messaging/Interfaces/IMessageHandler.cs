using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kwetter.Services.Shared.Interfaces
{
    /// <summary>
    /// The default message handler. Emits the received message data as <see cref="byte[]"/>
    /// </summary>
    public interface IMessageHandler
    {
        Task<bool> HandleMessageAsync(string messageType, string message);
    }

}
