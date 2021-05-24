using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.Shared.Messaging.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishMessageAsync<T>(string messageType, T value);
    }
}
