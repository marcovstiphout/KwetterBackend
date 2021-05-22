using Kwetter.Services.ProfileService.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.ProfileService.Application.Common.Interfaces.Services
{
    public interface IProfileService
    {
        Task<ProfileDTO> CreateProfileAsync(string profileName, Guid authId);
    }
}
