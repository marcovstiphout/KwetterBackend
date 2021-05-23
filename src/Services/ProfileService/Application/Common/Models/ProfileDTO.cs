using Kwetter.Services.ProfileService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.ProfileService.Application.Common.Models
{
    public class ProfileDTO
    {
        public Guid ProfileId { get; set; }
        public string ProfileName { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string ProfilePicture { get; set; }
        public static implicit operator ProfileDTO(Profile v)
        {
            ProfileDTO profile = new ProfileDTO()
            {
                ProfileId = v.ProfileId,
                ProfileName = v.ProfileName,
                Bio = v.Bio,
                Location = v.Location,
                Website = v.Website,
                ProfilePicture = v.ProfilePicture
            };
            return profile;
        }
    }
}
