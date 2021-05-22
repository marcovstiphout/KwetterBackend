using Kwetter.Services.ProfileService.Application.Common.Interfaces.Services;
using Kwetter.Services.ProfileService.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.ProfileService.Application.Services
{
    public class ProfileService : IProfileService
    {
        public Task<ProfileDTO> CreateProfileAsync(string profileName, Guid authId)
        {
            throw new NotImplementedException();
        }
    }
}
