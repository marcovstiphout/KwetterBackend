using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Application.Common.Models;
using Kwetter.Services.ProfileService.Domain;
using Kwetter.Services.Shared.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Kwetter.Services.ProfileService.Application.Handlers
{
    public class UserRegisteredMessageHandler : IMessageHandler
    {
        private readonly IProfileContext _context;

        public UserRegisteredMessageHandler(IProfileContext context)
        {
            _context = context;
        }
        public async Task<bool> HandleMessageAsync(string messageType, string message)
        {
            //Grab Values from Message and Register new Profile for the registered user
            UserDTO newUser = JsonConvert.DeserializeObject<UserDTO>(message);

            //Verify if User already has a Profile
            if (newUser == null) return false;

            Profile p = _context.Profiles.FindAsync(newUser.Id).Result;

            if (p == null)
            {
                Profile newProfile = new Profile()
                {
                    AuthId = newUser.Id,
                    ProfileName = newUser.Name,
                    ProfilePicture = newUser.Avatar
                };
                _context.Profiles.Add(newProfile);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
