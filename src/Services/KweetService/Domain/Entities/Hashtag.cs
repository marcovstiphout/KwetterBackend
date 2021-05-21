using System;
namespace Kwetter.Services.KweetService.Domain
{
    public class Hashtag
    {
        public Guid HashtagId { get; set; }
        public Guid KweetId { get; set; }
        public string HashtagText { get; set; }
    }
}
