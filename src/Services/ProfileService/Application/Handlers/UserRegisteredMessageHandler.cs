using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Application.Common.Models;
using Kwetter.Services.ProfileService.Domain;
using Kwetter.Services.Shared.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Kwetter.Services.ProfileService.Application.Handlers
{
    public class UserRegisteredMessageHandler : IMessageHandler<UserDTO>
    {
        private readonly IProfileContext _context;

        public UserRegisteredMessageHandler(IProfileContext context)
        {
            _context = context;
        }
        public async Task HandleMessageAsync(string messageType, UserDTO newUser)
        {
            //Verify if User already has a Profile
            if (newUser != null)
            {
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
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
