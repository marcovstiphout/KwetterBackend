using System;
using System.Collections.Generic;
using System.Text;

namespace Kwetter.Services.KweetService.Domain.Entities
{
    public class Profile
    {
        public Guid ProfileId { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
    }
}
