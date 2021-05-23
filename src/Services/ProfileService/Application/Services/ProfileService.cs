using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Application.Common.Interfaces.Services;
using Kwetter.Services.ProfileService.Application.Common.Models;
using Kwetter.Services.ProfileService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.ProfileService.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileContext _profileContext;
        public ProfileService(IProfileContext context)
        {
            _profileContext = context;
        }
        public async Task<ProfileDTO> CreateProfileAsync(string profileName, Guid authId)
        {
            Profile profileToCreate = new Profile()
            {
                ProfileId = Guid.NewGuid(),
                ProfileName = profileName,
                AuthId = authId
            };
            await _profileContext.Profiles.AddAsync(profileToCreate);
            bool success = await _profileContext.SaveChangesAsync() > 0;

            if (!success)
            {
                throw new InvalidOperationException("Something went wrong trying to create the player");
            }
            return profileToCreate;
        }
    }
}
