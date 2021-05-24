using Kwetter.Services.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.ProfileService.Application.Handlers
{
    public class UserRegisteredMessageHandler : IMessageHandler<string>
    {
        public Task HandleMessageAsync(string messageType, string message)
        {
            //Grab Values from Message and Register new Profile for the registered user
            throw new NotImplementedException();
        }
    }
}
