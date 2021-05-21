using System;
using System.Collections.Generic;
using System.Text;

namespace Kwetter.Services.KweetService.Domain
{
    public class Kweet
    {
        public Guid KweetId { get; set; }

        public Guid SenderProfileId { get; set; }

        public string KweetMessage { get; set; }

        public DateTime KweetPostDate { get; set; }
    }
}
