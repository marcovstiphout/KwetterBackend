using System;

namespace Kwetter.Services.ProfileService.Domain
{
    public class Profile
    {
        public Guid ProfileId { get; set; }
        public Guid AuthId { get; set; }
        public string ProfileName { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string ProfilePicture { get; set; }
    }
}
