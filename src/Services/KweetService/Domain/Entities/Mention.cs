using System;
using System.Collections.Generic;
using System.Text;

namespace Kwetter.Services.KweetService.Domain
{
    public class Mention
    {
        public Guid MentionId { get; set; }
        public Guid MentionProfileId { get; set; }
        public Guid KweetId { get; set; }
        public string MentionText { get; set; }
    }
}
