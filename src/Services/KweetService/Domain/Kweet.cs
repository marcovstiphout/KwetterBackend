using System;
using System.Collections.Generic;
using System.Text;

namespace Kwetter.Services.KweetService.Domain
{
    public class Kweet
    {
        public Guid Id { get; set; }

        public string KweetMessage { get; set; }

        public DateTime KweetPostDate { get; set; }
    }
}
